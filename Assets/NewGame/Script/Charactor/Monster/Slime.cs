namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using UnityEditor.Tilemaps;

    //UnityEngine
    using UnityEngine;

    public class Slime : Monster
    {
        [Header("Move")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpPower;
        [SerializeField] private float jumpCooltime;


        //private variables
        
        
        private void OnEnable()
        {
            Init();
            Jump();
            MonsterMovement();
        }

        private void Update()
        {
            FlipX();
            anim.SetFloat("yPos", rb.velocity.y);
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            rb.velocity = new Vector2(MonsterBehavior * moveSpeed, rb.velocity.y);
        }

        protected override void FlipX()
        {
            base.FlipX();
        }

        protected override int MonsterMovement()
        {
            return base.MonsterMovement();
        }

        private void Jump()
        {
            StartCoroutine(Co_Jump());
        }

        private IEnumerator Co_Jump()
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * MonsterBehavior * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJump", true);
            yield return new WaitForSeconds(jumpCooltime);
            StartCoroutine(Co_Jump());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                anim.SetBool("IsJump", false);
            }
            
        }
    }
}


