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
    [SerializeField] private GameObject settingBackGround = null;
    [SerializeField] private GameObject settingPanel = null;
    [SerializeField] private GameObject audioPanel = null;
    //[SerializeField] private GameObject videoPanel = null;
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
            settingBackGround.SetActive(true);
            settingPanel.SetActive(true);
            Time.timeScale = 0;
            isON = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isON == true && isAudioPanelOn == false)
        {

            settingBackGround.SetActive(false);
            settingPanel.SetActive(false);
            Time.timeScale = 1;
            isON = false;
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && isAudioPanelOn == true)
        {
            audioPanel.SetActive(false);
            settingPanel.SetActive(true);
            isAudioPanelOn = false;
        }
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);

    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXvolume(sfxSlider.value);
    }

    public void AudioPanelOn()
    {
        settingPanel.SetActive(false);

        audioPanel.SetActive(true);
        isAudioPanelOn = true;
    }

    //public void VideoPanelOn()
    //{

    //}

    public void RePlayButton()
    {
        LoadingScreenManager.LoadScene("MainInGame");
        Time.timeScale = 1;
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("MainTitle");
        Time.timeScale = 1;

    }
}
