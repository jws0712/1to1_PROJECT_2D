namespace OTO.Manager
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.UI;

    public class WaveManager : MonoBehaviour
    {
        public static WaveManager instance = null;

        [System.Serializable]
        public struct WaveMonster
        {
            public GameObject[] monsterArray;
        }
        public List<WaveMonster> waveList = new List<WaveMonster>();
        [SerializeField] private Transform[] spawnPosArray = null;

        
        [SerializeField] private float delayTime = default;
        [SerializeField] private float waveDelayTime = default;
        [SerializeField] private GameObject waveText = null;
        [SerializeField] private TextMeshProUGUI waveNumber = null;

        private WaitForSeconds waitForSeconds = null;
        private int waveCount = default;

        private void Awake()
        {
            instance = this;
            waitForSeconds = new WaitForSeconds(waveDelayTime);
        }

        private void Start()
        {
            WaveStart(delayTime);

            waveCount = 0;
        }

        public void WaveStart(float delayTime)
        {
            StartCoroutine(WaveLogic(delayTime));
        }

        private IEnumerator WaveLogic(float delayTime)
        {
            while(waveCount < waveList.Count)
            {
                if (GameManager.instance.isFieldClear == true)
                {
                    waveText.SetActive(false);
                    GameManager.instance.isStoreOpen = true;
                    yield return waitForSeconds;
                    GameManager.instance.isStoreOpen = false;
                    waveText.SetActive(true);
                    waveNumber.text = (waveCount + 1).ToString();
                    SpawnMonster(waveCount, delayTime);
                    waveCount+=1;
                }
                yield return null;
            }
        }

        public void SpawnMonster(int currentWaveCount , float delayTime)
        {
            StartCoroutine(Co_SpawnMonsterDelay(currentWaveCount, delayTime));
        }

        private IEnumerator Co_SpawnMonsterDelay(int currentWaveCount, float delayTime)
        {
            GameManager.instance.fieldMonsterCount = waveList[currentWaveCount].monsterArray.Length;
            for (int i = 0; i < waveList[currentWaveCount].monsterArray.Length; i++)
            {
                int spawnPosNumber = Random.Range(0, spawnPosArray.Length);
                Instantiate(waveList[currentWaveCount].monsterArray[i], spawnPosArray[spawnPosNumber].position, spawnPosArray[spawnPosNumber].rotation);
                yield return new WaitForSeconds(delayTime);
            }
        }
    }
}

