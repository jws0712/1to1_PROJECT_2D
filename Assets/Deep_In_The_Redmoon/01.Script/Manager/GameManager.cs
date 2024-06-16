namespace OTO.Manager
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine.UI

    //UnityEngine
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        public bool isStoreOpen = false;
        public bool isFieldClear = true;
        public float fieldMonsterCount = default;
        public float cursePointCount = default;
        public float coin = default;


        
        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            CheckFieldMonster();
        }

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
    }
}