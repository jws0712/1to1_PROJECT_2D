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
        private float chaseRange;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private GameObject expDiamond = null;
        [SerializeField]
        private GameObject coinDiamond = null;
        [SerializeField]
        private LayerMask target = default;
        [SerializeField]
        private float monsterSacle = default;



        //Protected variables
        protected Animator anim = null;
        protected Rigidbody2D rb = null;

        //Private variables
        private Transform houseTransform = null;
        private Transform playerTrasnform = null;
        private bool isChasePlayer = false;
        private const float stopDistance = 1;


        protected virtual void OnEnable()
        {
            Init();

            houseTransform = GameObject.FindGameObjectWithTag("House").transform;
        }
        protected virtual void Update()
        {
            CheckRange(chaseRange);

            

            if (isChasePlayer == true)
            {
                ChaseLogic(playerTrasnform);
            }
            else
            {
                ChaseLogic(houseTransform);
            }
        }

        private void ChaseLogic(Transform chaseTransform)
        {
            float objectDistance = Mathf.Abs(chaseTransform.position.x - transform.position.x);

            if (objectDistance < stopDistance)
            {
                StopChase();
            }
            else
            {
                Chase(chaseTransform);
            }
        }

        private void CheckRange(float range)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, range, target);

            if (collider != null)
            {
                playerTrasnform = collider.transform;
                isChasePlayer = true;
            }
            else
            {
                isChasePlayer = false;
            }

        }

        private void Chase(Transform chaseTransform)
        {
            if (transform.position.x < chaseTransform.position.x)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(-monsterSacle, monsterSacle);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(monsterSacle, monsterSacle);
            }
        }

        private void StopChase()
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}