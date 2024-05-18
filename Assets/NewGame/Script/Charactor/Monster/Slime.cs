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
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float jumpPower = default;
        [SerializeField] private float jumpCooltime = default;
        [SerializeField] private float rayDistance = default;
        [SerializeField] private float rayPos = default;
        [SerializeField] private LayerMask layerMask = default;
        [SerializeField] private Vector2 rayDirection = default;

        [Header("Info")]
        [SerializeField] private float slimeHp = default;


        //private variables


        private void OnEnable()
        {
            Init();
            Jump();
            MonsterMovement();

            maxHp = slimeHp;
        }

        private void Update()
        {
            FlipX();
            CheckGround(rayDirection ,rayDistance, rayPos, layerMask);
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
            
            

            anim.SetBool("IsJump", true);
            yield return new WaitForSeconds(jumpCooltime);
            StartCoroutine(Co_Jump());
        }

        protected override void CheckGround(Vector2 direction, float distacne, float rayPos, LayerMask layerMask)
        {
            base.CheckGround(direction ,distacne, rayPos, layerMask);
            if (rayHit.collider != null)
            {
                MonsterBehavior *= -1;
            }
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


