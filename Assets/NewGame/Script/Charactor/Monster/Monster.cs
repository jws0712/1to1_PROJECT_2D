namespace OTO.Charactor.Monster
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    public class Monster : Charactor
    {
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private float agroRange;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private GameObject expDiamond = null;
        [SerializeField]
        private GameObject coinDiamond = null;

        //Protected variables
        protected Animator anim = null;
        protected Rigidbody2D rb;


        protected virtual void OnEnable()
        {
            Init();
        }
        protected virtual void Update()
        {
            float playerDistance = Vector2.Distance(transform.position, playerTransform.position);

            if (playerDistance < agroRange)
            {
                if (playerDistance < 0.5f)
                {
                    StopPlayerChase();
                    Debug.Log("╫╨е╬га");
                }
                else
                {
                    PlayerChase();
                }
            }

            else
            {
                StopPlayerChase();
                
            }
        }
        private void Init()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        protected override void Die()
        {
            base.Die();

            Destroy(gameObject);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        private void PlayerChase()
        {
            if (transform.position.x < playerTransform.position.x)
            {
                rb.velocity = Vector2.right * moveSpeed;
                transform.localScale = new Vector2(-0.8f, 0.8f);
            }
            else
            {
                rb.velocity = Vector2.left * moveSpeed;
                transform.localScale = new Vector2(0.8f, 0.8f);
            }
        }

        private void StopPlayerChase()
        {
            rb.velocity = Vector2.zero;
        }
    }
}