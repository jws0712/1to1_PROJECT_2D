namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using Unity.VisualScripting;
    using UnityEditorInternal;

    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Object;
    using System.Linq.Expressions;

    /// <summary>
    /// �������� �ൿ�� �����ϴ� �Լ�
    /// </summary>
    public class Slime : Monster
    {
        [Header("SlimeJump")]
        [SerializeField] private float jumpPower = default;
        [SerializeField] private LayerMask gorundLayer = default;
        [SerializeField] private Transform groundCheckPos = default;

        //public variables
        private bool isHouseAttack = default;

        /// <summary>
        /// �ʱ�ȭ
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            chaseHouse = true; //���� �߰���
            currentCoolTime = 0f;
        }

        /// <summary>
        /// ���� Ŭ������ ������Ʈ�� ������
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// �������� ������ �����ϴ� �Լ�
        /// </summary>
        protected override void Attack()
        {
            base.Attack();
            if (isAttack == true)
            {

                if (CheckGround() == true)
                {
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    isHouseAttack = true;
                }
                isAttack = false;
                currentCoolTime = 0;
            }
        }

        /// <summary>
        /// �������� ���� ��Ҵ��� �˻��ϴ� �Լ�
        /// </summary>
        /// <returns></returns>
        private bool CheckGround()
        {
            return Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, gorundLayer);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.CompareTag("House") && isHouseAttack == true)
            {
                collision.gameObject.GetComponent<Shop>().TakeDamage(bodyDamage);
                isHouseAttack = false;
            }
        }
    }
}


