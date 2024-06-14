namespace OTO.Manager
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;



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
        //public int currentWaveListIndex = 0;
        //public float delayTime = default;

        private void Awake()
        {
            instance = this;
        }

        public void SpawnMonster(int currentWaveCount , float delayTime)
        {
            StartCoroutine(Co_SpawnMonsterDelay(currentWaveCount, delayTime));
        }

        private IEnumerator Co_SpawnMonsterDelay(int currentWaveCount, float delayTime)
        {
            for (int i = 0; i < waveList[currentWaveCount].monsterArray.Length; i++)
            {
                int spawnPosNumber = Random.Range(0, spawnPosArray.Length);
                Debug.Log(spawnPosNumber);
                Instantiate(waveList[currentWaveCount].monsterArray[i], spawnPosArray[spawnPosNumber].position, spawnPosArray[spawnPosNumber].rotation);

                yield return new WaitForSeconds(delayTime);
            }
        }


    }
}

