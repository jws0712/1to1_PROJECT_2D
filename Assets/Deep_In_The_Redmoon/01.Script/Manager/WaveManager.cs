namespace OTO.Manager
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// ���̺긦 �����ϴ� Ŭ����
    /// </summary>
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager instance = null;

        public List<WaveData> waves = new List<WaveData>(); //�� ���̺꿡 ���� ���� �迭�� ���� ����Ʈ

        [SerializeField] private Transform[] spawnPosArray = null;
        
        [SerializeField] private float spawnDelayTime = default;
        [SerializeField] private float waveDelayTime = default;
        [SerializeField] private GameObject waveText = null;
        [SerializeField] private TextMeshProUGUI waveNumber = null;

        private WaitForSeconds waitForSeconds = null;
        private int waveCount = default;
        private bool isWaveStart = false;

        private void Awake() //�̱���
        {
            instance = this;
            waitForSeconds = new WaitForSeconds(waveDelayTime);
        }

        /// <summary>
        /// �ʱ�ȭ
        /// </summary>
        private void Start()
        {
            waveCount = 0;
        }

        /// <summary>
        /// ���� ���¸� �˻��ϴ� �ڵ�
        /// </summary>
        private void Update()
        {

            if (GameManager.instance.isPlayerSpawn == true && isWaveStart == false)
            {
                WaveStart(spawnDelayTime);
                isWaveStart = true;
            }

            if(GameManager.instance.fieldMonsterCount == 0 && waveCount == waves.Count)
            {
                GameManager.instance.Clear();
            }

        }



        /// <summary>
        /// ���̺긦 �����ϴ� �Լ�
        /// </summary>
        /// <param name="delayTime"></param>
        public void WaveStart(float delayTime)
        {
            StartCoroutine(WaveLogic(delayTime));
        }

        /// <summary>
        /// ���̺��� �˰����� ������ �ڷ�ƾ
        /// </summary>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        private IEnumerator WaveLogic(float delayTime)
        {
            while(waveCount < waves.Count)
            {
                if (GameManager.instance.isFieldClear == true)
                {
                    waveText.SetActive(false);
                    GameManager.instance.isStoreOpen = true;
                    yield return waitForSeconds;
                    GameManager.instance.isStoreOpen = false;
                    waveText.SetActive(true);
                    waveNumber.text = (waveCount).ToString();
                    SpawnMonster(waveCount, delayTime);
                    waveCount++;
                    
                }
                yield return null;
            }
        }

        /// <summary>
        /// ���͸� ������Ű�� �Լ�
        /// </summary>
        /// <param name="currentWaveCount"></param>
        /// <param name="delayTime"></param>
        public void SpawnMonster(int currentWaveCount , float delayTime)
        {
            StartCoroutine(Co_SpawnMonsterDelay(currentWaveCount, delayTime));
        }

        /// <summary>
        /// ���͸� ������Ű�� �ڷ�ƾ
        /// </summary>
        /// <param name="currentWaveCount"></param>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        private IEnumerator Co_SpawnMonsterDelay(int currentWaveCount, float delayTime)
        {
            GameManager.instance.fieldMonsterCount = waves[currentWaveCount].monsterArray.Length;
            for (int i = 0; i < waves[currentWaveCount].monsterArray.Length; i++)
            {
                int spawnPosNumber = Random.Range(0, spawnPosArray.Length);
                Instantiate(waves[currentWaveCount].monsterArray[i], spawnPosArray[spawnPosNumber].position, spawnPosArray[spawnPosNumber].rotation);
                yield return new WaitForSeconds(delayTime);
            }
        }
    }
}

