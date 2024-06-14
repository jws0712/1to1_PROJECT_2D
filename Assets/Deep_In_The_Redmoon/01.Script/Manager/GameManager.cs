namespace OTO.Manager
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        private void Awake()
        {
            instance = this;
        }

        [Header("WaveInfo")]
        public int currentWaveListIndex = 0;
        public float delayTime = default;
        

    }
}