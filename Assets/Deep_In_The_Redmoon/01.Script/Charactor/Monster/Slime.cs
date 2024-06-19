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
    using OTO.Bullet;

    public class Slime : Monster
    {
        [Header("SlimeJump")]
        [SerializeField]
        private float jumpPower = default;
        [SerializeField]
        private LayerMask gorundLayer = default;
        [SerializeField]
        private Transform groundCheckPos = default;

        private bool isHouseAttack = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            currentCoolTime = 0f;
        }

        protected override void Update()
        {
            base.Update();
        }
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


