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
    /// 웨이브를 관리하는 클래스
    /// </summary>
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager instance = null;

        [System.Serializable]
        public struct WaveMonster
        {
            public GameObject[] monsterArray; //몬스터를 담을 배열
        }

        public List<WaveMonster> waveList = new List<WaveMonster>(); //한 웨이브에 나올 몬스터 배열을 담은 리스트
        [SerializeField] private Transform[] spawnPosArray = null;

        
        [SerializeField] private float spawnDelayTime = default;
        [SerializeField] private float waveDelayTime = default;
        [SerializeField] private GameObject waveText = null;
        [SerializeField] private TextMeshProUGUI waveNumber = null;

        private WaitForSeconds waitForSeconds = null;
        private int waveCount = default;
        private bool isWaveStart = false;

        private void Awake() //싱글톤
        {
            instance = this;
            waitForSeconds = new WaitForSeconds(waveDelayTime);
        }

        /// <summary>
        /// 초기화
        /// </summary>
        private void Start()
        {
            waveCount = 0;
        }

        /// <summary>
        /// 현재 상태를 검사하는 코드
        /// </summary>
        private void Update()
        {
            Debug.Log(GameManager.instance.fieldMonsterCount);
            Debug.Log(waveCount + "웨이브 카운트");

            if (GameManager.instance.isPlayerSpawn == true && isWaveStart == false)
            {
                WaveStart(spawnDelayTime);
                isWaveStart = true;
            }

            if(waveCount - 1 > waveList.Count && GameManager.instance.isStoreOpen == false)
            {
                GameManager.instance.Clear();
            }
        }

        /// <summary>
        /// 웨이브를 시작하는 함수
        /// </summary>
        /// <param name="delayTime"></param>
        public void WaveStart(float delayTime)
        {
            StartCoroutine(WaveLogic(delayTime));
        }

        /// <summary>
        /// 웨이브의 알고리즘을 구현한 코루틴
        /// </summary>
        /// <param name="delayTime"></param>
        /// <returns></returns>
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
                    waveCount++;
                }
                yield return null;
            }
        }

        /// <summary>
        /// 몬스터를 스폰시키는 함수
        /// </summary>
        /// <param name="currentWaveCount"></param>
        /// <param name="delayTime"></param>
        public void SpawnMonster(int currentWaveCount , float delayTime)
        {
            StartCoroutine(Co_SpawnMonsterDelay(currentWaveCount, delayTime));
        }

        /// <summary>
        /// 몬스터를 스폰시키는 코루틴
        /// </summary>
        /// <param name="currentWaveCount"></param>
        /// <param name="delayTime"></param>
        /// <returns></returns>
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

