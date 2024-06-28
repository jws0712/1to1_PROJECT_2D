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
    /// ������ ��ü������ �����ϴ� Ŭ����
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

        private void Awake() //�̱���
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
        /// ������ ���¸� �����ϴ� �ڵ�
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
        /// �ʵ忡�� ���Ͱ� �ִ��� ������ �����ϴ� �Լ�
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
        /// ������ ������� ����Ǵ� �Լ�
        /// </summary>
        public void GetCoin()
        {
            coinCount += coinVel;
        }

        /// <summary>
        /// ���� ������ ������ ����Ǵ� �Լ�
        /// </summary>
        public void GameOver()
        {
            gameOverPanel.SetActive(true);
            isGameOver = true;
            Time.timeScale = 0;
        }

        /// <summary>
        /// ������ Ŭ���� ������ ����Ǵ� �Լ�
        /// </summary>
        public void Clear()
        {
            gameClearPanel.SetActive(true);
            isGameClear = true;
            Time.timeScale = 0;
        }

        /// <summary>
        /// ����ϱ� ��ư�� ����� �ϴ� �Լ�
        /// </summary>
        public void MoveNextScene()
        {
            Scene scene = SceneManager.GetActiveScene();

            LoadingScreenManager.LoadScene((scene.buildIndex + 1).ToString());
        }

        /// <summary>
        /// ���� ���� �˻��ؼ� ������ �����ϴ� �Լ�
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
            }
        }
    }
}