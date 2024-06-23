namespace OTO.Charactor.Monster
{
    using OTO.Charactor.Player;

    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    //UnityEngine
    using UnityEngine;

    //Project
    using Bullet;
    using UnityEngine.LowLevel;
    using Unity.VisualScripting;

    public class SlimeBoss : Monster
    {
        [Header("BossSlimeJump")]
        [SerializeField] private float jumpPower = default;
        [SerializeField] private LayerMask gorundLayer = default;
        [SerializeField] private Transform groundCheckPos = default;
        [SerializeField] private float dashPower = default;

        [Header("SlimeBossInfo")]
        [SerializeField]
        private GameObject bulletObject = null;
        [SerializeField]
        private int bulletNumber = default;
        [SerializeField]
        private int bulletSpeadAngle = default;
        [SerializeField]
        private int startBulletSpreadAngle = default;

        //public variables
        [HideInInspector]
        private bool isHouseAttack = default;

        //private variables
        private Animator animator = null;

        protected override void OnEnable()
        {
            chaseHouse = false;
            base.OnEnable();
            animator = GetComponent<Animator>();
            currentCoolTime = 0f;
        }

        protected override void Update()
        {
            base.Update();

            animator.SetFloat("YPos", rb.velocity.y);

            if(CheckGround() == false)
            {
                animator.SetBool("isJump", true);
            }
            else
            {
                animator.SetBool("isJump", false);
            }
        }

        /// <summary>
        /// 슬라임의 공격을 실행하는 함수
        /// </summary>
        protected override void Attack()
        {
            base.Attack();
            if (isAttack == true)
            {
                
                Jump();

                isAttack = false;
                currentCoolTime = 0;
            }
        }


        /// <summary>
        /// 총알 퍼짐을 구현한 코드
        /// </summary>
        private void bulletStorm()
        {
            float bulletSpread = transform.rotation.z + startBulletSpreadAngle;
            for (int i = 0; i < bulletNumber; i++)
            {
                Quaternion bulletAngle = Quaternion.Euler(0, 0, bulletSpread);
                GameObject _bullet = Instantiate(bulletObject, transform.position, bulletAngle);
                _bullet.GetComponent<Bullet>().bulletDamage = bulletDamage;
                bulletSpread -= bulletSpeadAngle;

            }
            bulletSpread = transform.rotation.z + startBulletSpreadAngle * 2;
        }

        /// <summary>
        /// 점프를 실행하는 코드
        /// </summary>
        private void Jump()
        {
            if (CheckGround() == true)
            {
                AudioManager.instance.PlaySFX("SlimeBossJump");
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                bulletStorm();
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                AudioManager.instance.PlaySFX("BossLanding");
                CameraShakeManager.instance.PlayShake("BossLanding");
                bulletStorm();
            }
        }
    }
}
