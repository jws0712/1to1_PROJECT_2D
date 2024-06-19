using OTO.Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class In_Game_UI_Manager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject settingBackGround = null;
    [SerializeField] private GameObject settingPanel = null;
    [SerializeField] private GameObject audioPanel = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;

    private bool isON = default;
    private bool isAudioPanelOn = default;

    private void Start()
    {
        isON = false;
    }
    private void Update()
    {
        if(GameManager.instance.isGameOver == true)
        {
            settingBackGround.SetActive(false);
            settingPanel.SetActive(false);
            audioPanel.SetActive(false);
            return;
        }

        SettingPanelOnOff();
    }

    private void SettingPanelOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isON == false)
        {
            AudioManager.instance.PlaySFX("ButtonClick");

            settingBackGround.SetActive(true);
            settingPanel.SetActive(true);
            Time.timeScale = 0;
            isON = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isON == true && isAudioPanelOn == false)
        {
            AudioManager.instance.PlaySFX("ButtonClick");

            settingBackGround.SetActive(false);
            settingPanel.SetActive(false);
            Time.timeScale = 1;
            isON = false;
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && isAudioPanelOn == true)
        {
            AudioManager.instance.PlaySFX("ButtonClick");

            audioPanel.SetActive(false);
            settingPanel.SetActive(true);
            isAudioPanelOn = false;
        }
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXvolume(sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void AudioPanelOn()
    {
        settingPanel.SetActive(false);

        audioPanel.SetActive(true);
        isAudioPanelOn = true;
    }

    public void RePlayButton()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        LoadingScreenManager.LoadScene("MainInGame");
    }

    public void TitleButton()
    {
        AudioManager.instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene("MainTitle");
        Time.timeScale = 1;
    }

    public void mouseEnterEvent()
    {
        AudioManager.instance.PlaySFX("MouseEnter");
    }
}
