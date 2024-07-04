namespace OTO.Manager
{

    //UnityEngine
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Audio;

    /// <summary>
    /// 아웃게임의 UI를 관리하는 클래스
    /// </summary>
    public class Out_Game_UI_Manager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject settingBackGround = null;
        [SerializeField] private GameObject settingPanel = null;
        [SerializeField] private GameObject audioPanel = null;
        [SerializeField] private GameObject tipPanel = null;
        [SerializeField] private GameObject craditPanel = null;
        [SerializeField] private GameObject buttons = null;

        [Header("Audio")]
        [SerializeField] private AudioMixer audioMixer = null;
        [SerializeField] private Slider musicSlider = null;
        [SerializeField] private Slider sfxSlider = null;

        private bool isON = default;
        private bool isAudioPanelOn = default;

        /// <summary>
        /// 소리조절 슬라이더 초기화
        /// </summary>
        private void Start()
        {
            Time.timeScale = 1;

            Cursor.visible = true;

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

        /// <summary>
        /// 패널을 끄는 기능을 구현한 함수
        /// </summary>
        private void Update()
        {
            SettingPanelOnOff();
        }

        /// <summary>
        /// 시작버튼의 기능을 구현한 함수
        /// </summary>
        public void StartButton()
        {
            AudioManager.instance.PlaySFX("PressStartButton");
            LoadingScreenManager.LoadScene("2");
        }

        /// <summary>
        /// 설정버튼을 구현한 함수
        /// </summary>
        public void SettingButton()
        {
            settingBackGround.SetActive(true);
            settingPanel.SetActive(true);
            buttons.SetActive(false);
            Time.timeScale = 0;
            isON = true;
        }

        /// <summary>
        /// 열려있는 패널을 비활성화 하는 코드
        /// </summary>
        private void SettingPanelOnOff()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isON == true && isAudioPanelOn == false)
            {
                settingBackGround.SetActive(false);
                settingPanel.SetActive(false);
                tipPanel.SetActive(false);
                craditPanel.SetActive(false);
                buttons.SetActive(true);
                Time.timeScale = 1;
                isON = false;
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && isAudioPanelOn == true)
            {
                audioPanel.SetActive(false);
                settingPanel.SetActive(true);
                isAudioPanelOn = false;
            }
        }

        /// <summary>
        /// 오디오 버튼의 기능을 구현한 함수
        /// </summary>
        public void AudioPanelOn()
        {
            buttons.SetActive(false);
            settingPanel.SetActive(false);
            audioPanel.SetActive(true);
            isAudioPanelOn = true;
        }

        /// <summary>
        /// 도움말 기능을 구현한 함수
        /// </summary>
        public void TipButton()
        {
            tipPanel.SetActive(true);
            isON = true;
        }

        /// <summary>
        /// 크래딧 버튼의 기능을 구현한 함수
        /// </summary>
        public void CraditButton()
        {
            craditPanel.SetActive(true);
            isON = true;

        }

        /// <summary>
        /// 나가기 버튼의 기능을 구현한 함수
        /// </summary>
        public void QuitButtonButton()
        {
            Application.Quit();
        }
    }
}
