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
    /// ������ ����� ������ �Լ�
    /// </summary>
    public class Shop : MonoBehaviour
    {
        [SerializeField]
        private float maxHp = default;
        [SerializeField] private Slider houseHpSlider = null;

        public float currentHp = default;

        private Animator animator = null;

        /// <summary>
        /// ������Ʈ �ʱ�ȭ
        /// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// ü�� �ʱ�ȭ
        /// </summary>
        private void Start()
        {
            currentHp = maxHp;
        }

        /// <summary>
        /// ü��UI�� ü���� �����ϴ� �ڵ�
        /// </summary>
        private void Update()
        {
            houseHpSlider.value = currentHp / maxHp;
            animator.SetBool("isOpen", GameManager.instance.isStoreOpen);

            if (GameManager.instance.isFieldClear == true)
            {
                currentHp = maxHp;
            }

            if (currentHp <= 0) //ü���� 0�� �Ǹ� ���ӿ����� �ǰ���
            {
                GameManager.instance.GameOver();
            }
        }

        /// <summary>
        /// ������� ������ ����Ǵ� �Լ�
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            currentHp -= damage;
        }
    }
}


