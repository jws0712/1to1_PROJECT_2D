using OTO.Manager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// 인게임의 UI를 관리하는 클래스
/// </summary>
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

    /// <summary>
    /// 소리조절 슬라이더 초기화
    /// </summary>
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

    /// <summary>
    /// 게임 오버 됐을때 코드 
    /// </summary>
    private void Update()
    {
        if(GameManager.instance.isGameOver == true || GameManager.instance.isGameClear == true)
        {
            return;
        }

        if(GameManager.instance.isGameOver == true)
        {
            settingBackGround.SetActive(false);
            settingPanel.SetActive(false);
            audioPanel.SetActive(false);
        }
        else
        {
            SettingPanelOnOff();
        }
    }

    /// <summary>
    /// 열려있는 패널을 비활성화 하는 코드
    /// </summary>
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

    /// <summary>
    /// 오디오 패널을 활성화 하는 코드
    /// </summary>
    public void AudioPanelOn()
    {
        settingPanel.SetActive(false);
        audioPanel.SetActive(true);
        isAudioPanelOn = true;
    }

    /// <summary>
    /// 리플레이 버튼 구현한 함수
    /// </summary>
    public void RePlayButton()
    {
        Scene scene = SceneManager.GetActiveScene();

        LoadingScreenManager.LoadScene(scene.name);
    }

    /// <summary>
    /// 타이틀버튼의 기능을 구현한 함수
    /// </summary>
    public void TitleButton()
    {
        SceneManager.LoadScene("MainTitle");
        Time.timeScale = 1;
    }
}
