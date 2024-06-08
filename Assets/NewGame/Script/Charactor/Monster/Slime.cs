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


        protected override void OnEnable()
        {

            base.OnEnable();
            Jump();
            MonsterMovement();

            maxHp = slimeHp;
        }

        protected override void Update()
        {
            base.Update();

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
            while(!isDead) 
            {
                anim.SetBool("IsJump", true);
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                yield return new WaitForSeconds(jumpCooltime);
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


