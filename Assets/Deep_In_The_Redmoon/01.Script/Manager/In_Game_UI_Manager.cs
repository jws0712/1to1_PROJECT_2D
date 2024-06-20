using OTO.Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEditor.Experimental.RestService;

public class In_Game_UI_Manager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject settingBackGround = null;
    [SerializeField] private GameObject settingPanel = null;
    [SerializeField] private GameObject audioPanel = null;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;

    private bool isON = default;
    private bool isAudioPanelOn = default;

    private void Start()
    {
        isON = false;

        musicSlider.onValueChanged.AddListener((value) => AudioManager.instance.SetMusicVolume(value, audioMixer));
        sfxSlider.onValueChanged.AddListener((value) => AudioManager.instance.SetSFXVolume(value, audioMixer));


        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("SFXVolume"))
        {
            AudioManager.instance.LoadVolume(musicSlider.value, sfxSlider.value, audioMixer);

            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        }
        else
        {
            AudioManager.instance.SetMusicVolume(musicSlider.value, audioMixer);
            AudioManager.instance.SetSFXVolume(sfxSlider.value, audioMixer);
        }
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

    public void AudioPanelOn()
    {
        settingPanel.SetActive(false);
        audioPanel.SetActive(true);
        isAudioPanelOn = true;
    }

    public void RePlayButton()
    {
        LoadingScreenManager.LoadScene("MainInGame");
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("MainTitle");
        Time.timeScale = 1;
    }

    public void mouseEnterEvent()
    {

    }
}
