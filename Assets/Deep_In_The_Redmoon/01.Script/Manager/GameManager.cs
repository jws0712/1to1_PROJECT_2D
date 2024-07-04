namespace OTO.Manager
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using Unity.VisualScripting;


    //UnityEngine
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// 게임을 전체적으로 관리하는 클래스
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        [Header("Info")]
        [SerializeField] private float coinVel = default;
        public float fieldMonsterCount = default;
        public bool isFieldClear = true;
        public bool isGameOver = false;
        public bool isGameClear = false;
        public bool isStoreOpen = false;
        public bool isPlayerSpawn = false;

        [Header("UI")]
        public TextMeshProUGUI coinText = null;
        public Slider hpSlider = null;
        [SerializeField] GameObject gameOverPanel = null;
        [SerializeField] GameObject gameClearPanel = null;

        [Header("UI_Info")]
        public float coinCount = default;

        private void Awake() //싱글톤
        {
            if(instance == null)
            {
                instance = this;
            }

        }

        private void Start()
        {
            CheckScene();
        }

        /// <summary>
        /// 게임의 상태를 관리하는 코드
        /// </summary>
        private void Update()
        {
            CheckFieldMonster();

            if(coinText != null)
            {
                coinText.text = coinCount.ToString();
            }
            

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                isPlayerSpawn = true;   
            }
            else
            {
                isPlayerSpawn = false;
            }
        }

        /// <summary>
        /// 필드에서 몬스터가 있는지 없는지 관리하는 함수
        /// </summary>
        private void CheckFieldMonster()
        {
            if(fieldMonsterCount == 0)
            {
                isFieldClear = true;
            }
            else
            {
                isFieldClear = false;
            }
        }

        /// <summary>
        /// 동전을 얻었을때 실행되는 함수
        /// </summary>
        public void GetCoin()
        {
            coinCount += coinVel;
        }

        /// <summary>
        /// 게임 오버가 됬을때 실행되는 함수
        /// </summary>
        public void GameOver()
        {
            AudioManager.instance.PlayMusic("GameOver");
            AudioManager.instance.PlaySFX("GameOver");
            gameOverPanel.SetActive(true);
            isGameOver = true;
            Time.timeScale = 0;
        }

        /// <summary>
        /// 게임을 클리어 했을때 실행되는 함수
        /// </summary>
        public void Clear()
        {
            gameClearPanel.SetActive(true);
            isGameClear = true;
            Time.timeScale = 0;
        }

        /// <summary>
        /// 계속하기 버튼의 기능을 하는 함수
        /// </summary>
        public void MoveNextScene()
        {
            Scene scene = SceneManager.GetActiveScene();

            LoadingScreenManager.LoadScene((scene.buildIndex + 1).ToString());
        }

        /// <summary>
        /// 현재 씬을 검사해서 음악을 실행하는 함수
        /// </summary>
        private void CheckScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            switch (scene.name)
            {
                case "MainTitle":
                    {
                        AudioManager.instance.PlayMusic("Title");
                        break;
                    }
                case "2":
                    {
                        AudioManager.instance.PlayMusic("Stage1");
                        break;
                    }

                case "3":
                    {
                        AudioManager.instance.PlayMusic("Stage2");
                        break;
                    }
                case "4":
                    {
                        AudioManager.instance.PlayMusic("Stage3");
                        break;
                    }
            }
        }
    }
}