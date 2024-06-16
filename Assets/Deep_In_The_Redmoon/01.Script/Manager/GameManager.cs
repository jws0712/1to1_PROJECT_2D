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

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        [Header("Info")]
        [SerializeField] private float coinVel = default;
        public float fieldMonsterCount = default;
        public bool isStoreOpen = false;
        public bool isFieldClear = true;

        [Header("UI")]
        public TextMeshProUGUI coinText = null;
        public Slider cursePointSlider = null;
        public Slider hpSlider = null;

        [Header("UI_Info")]
        public float maxCursePointCount = default;
        public float cursePointCount = default;
        public float coinCount = default;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            CheckFieldMonster();

            coinText.text = coinCount.ToString();

            cursePointSlider.value = cursePointCount / maxCursePointCount;

            if(cursePointCount >= maxCursePointCount)
            {
                cursePointCount = 0;
            }
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

        public void GetCoin()
        {
            coinCount += coinVel;

        }

        public void GetCursePoint()
        {
            cursePointCount += 1;
        }
    }
}