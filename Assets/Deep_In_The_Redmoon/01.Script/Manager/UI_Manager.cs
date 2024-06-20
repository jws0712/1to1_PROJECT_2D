namespace OTO.Manager
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using UnityEngine.Audio;

    public class UI_Manager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button startButton = null;
        [SerializeField] private Button settingButton = null;
        [SerializeField] private Button quitButton = null;

        [Header("Audio")]
        [SerializeField] private AudioMixer audioMixer = null;
        [SerializeField] private Slider musicSlider = null;
        [SerializeField] private Slider sfxSlider = null;

        private void Start()
        {
            Cursor.visible = true;

            startButton.onClick.AddListener(StartButton);
            settingButton.onClick.AddListener(SettingButton);
            quitButton.onClick.AddListener(QuitButtonButton);

            if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("SFXVolume"))
            {
                AudioManager.instance.LoadVolume(musicSlider, sfxSlider, audioMixer);
            }
            else
            {
                AudioManager.instance.SetMusicVolume(musicSlider, audioMixer);
                AudioManager.instance.SetSFXVolume(sfxSlider, audioMixer);
            }
        }

        private void StartButton()
        {
            LoadingScreenManager.LoadScene("MainInGame");
        }

        private void SettingButton()
        {

        }

        private void QuitButtonButton()
        {
            Application.Quit();
        }

        public void mouseEnterEvent()
        {

        }
    }
}
