namespace OTO.Object 
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.UI;

    //Project
    using OTO.Manager;

    /// <summary>
    /// 상점의 기능을 구현한 함수
    /// </summary>
    public class Shop : MonoBehaviour
    {
        [SerializeField]
        private float maxHp = default;
        [SerializeField] private Slider houseHpSlider = null;

        public float currentHp = default;

        private Animator animator = null;

        /// <summary>
        /// 컴포넌트 초기화
        /// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// 체력 초기화
        /// </summary>
        private void Start()
        {
            currentHp = maxHp;
        }

        /// <summary>
        /// 체력UI와 체력을 관리하는 코드
        /// </summary>
        private void Update()
        {
            houseHpSlider.value = currentHp / maxHp;
            animator.SetBool("isOpen", GameManager.instance.isStoreOpen);

            if (GameManager.instance.isFieldClear == true)
            {
                currentHp = maxHp;
            }

            if (currentHp <= 0) //체력이 0이 되면 게임오버가 되게함
            {
                GameManager.instance.GameOver();
            }
        }

        /// <summary>
        /// 대미지를 받을때 실행되는 함수
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            currentHp -= damage;
        }
    }
}


