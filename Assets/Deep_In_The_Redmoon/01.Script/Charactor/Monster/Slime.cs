namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Object;
    using System.Linq.Expressions;
    using OTO.Manager;

    /// <summary>
    /// 슬라임의 행동을 관리하는 함수
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
        /// 초기화
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            chaseHouse = true; //집을 추격함
            currentCoolTime = 0f;
        }

        /// <summary>
        /// 몬스터 클래스의 업데이트를 가져옴
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 슬라임의 공격을 실행하는 함수
        /// </summary>
        protected override void Attack()
        {
            base.Attack();
            if (isAttack == true)
            {

                if (CheckGround() == true)
                {
                    AudioManager.instance.PlaySFX("MiniSlimeJump");
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    isHouseAttack = true;
                }
                isAttack = false;
                currentCoolTime = 0;
            }
        }

        /// <summary>
        /// 슬라임이 땅에 닿았는지 검사하는 함수
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


