namespace OTO.Charactor.Monster
{
    

    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Object;
    using OTO.Manager;
    using OTO.Charactor.Player;

    public class SlimeBoss : Monster
    {
        [Header("BossSlimeJump")]
        [SerializeField] private float jumpPower = default;
        [SerializeField] private LayerMask gorundLayer = default;
        [SerializeField] private Transform groundCheckPos = default;

        [Header("SlimeBossInfo")]
        [SerializeField]
        private GameObject bulletObject = null;
        [SerializeField]
        private int bulletNumber = default;
        [SerializeField]
        private int bulletSpeadAngle = default;
        [SerializeField]
        private int startBulletSpreadAngle = default;

        //private variables
        private Animator animator = null;


        /// <summary>
        /// 초기화
        /// </summary>
        protected override void OnEnable()
        {
            chaseHouse = false; //집을 추격하지 않음
            base.OnEnable();
            animator = GetComponent<Animator>();
            currentCoolTime = 0f;
        }

        /// <summary>
        /// 몬스터 클래스의 업데이트를 실행시키고 슬라임보스의 애니매이션를 관리함
        /// </summary>
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
        private void FireBullet()
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
                FireBullet();
            }
        }
        /// <summary>
        /// 보스슬라임이 땅에 닿았는지 검사하는 함수
        /// </summary>
        /// <returns></returns>
        private bool CheckGround()
        {
            return Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, gorundLayer);
        }

        /// <summary>
        /// 땅과 부딛쳤을때 실행되는 코드
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.gameObject.GetComponent<PlayerManager>().TakeDamage(bodyDamage);
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                AudioManager.instance.PlaySFX("BossLanding");
                CameraShakeManager.instance.PlayShake("BossLanding");
                FireBullet();
            }
        }
    }
}
