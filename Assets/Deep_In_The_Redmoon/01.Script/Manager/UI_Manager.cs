namespace OTO.Manager
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class UI_Manager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button startButton = null;
        [SerializeField] private Button settingButton = null;
        [SerializeField] private Button quitButton = null;

        private void Start()
        {
            Cursor.visible = true;

            startButton.onClick.AddListener(StartButton);
            settingButton.onClick.AddListener(SettingButton);
            quitButton.onClick.AddListener(QuitButtonButton);
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
