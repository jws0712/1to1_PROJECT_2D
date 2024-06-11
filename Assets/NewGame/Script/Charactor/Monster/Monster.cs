namespace OTO.Charactor.Monster
{
    using OTO.Charactor.Player;
    //System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    public class Monster : Charactor
    {
        [Header("MonsterInfo")]
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float chaseRange = default;
        [SerializeField] private float attackRange = default;
        [SerializeField] private float attackCoolTime = default;
        [SerializeField] private float monsterSacle = default;
        [SerializeField] private LayerMask chaseTarget = default;


        [Header("Damage")]
        [SerializeField] public float bulletDamage = default;
        [SerializeField] public float bodyDamage = default;

        [Header("DropItem")]
        [SerializeField] private GameObject expDiamond = null;
        [SerializeField] private GameObject coinDiamond = null;



        //Protected variables
        protected Animator anim = null;
        protected Rigidbody2D rb = null;
        protected Transform playerTrasnform = null;
        protected Transform houseTransform = null;
        protected bool isChasePlayer = false;
        //protected bool isChaseShop = false;
        protected bool isAttack = default;
        protected float currentCoolTime = default;

        //Private variables
        private const float stopDistance = 1;


        protected virtual void OnEnable()
        {
            Init();
            houseTransform = GameObject.FindGameObjectWithTag("House").transform;
            currentCoolTime = 0f;


        }
        protected virtual void Update()
        {
            CheckRange(chaseRange);
            CheackAttackDistance();

            if (isChasePlayer == true)
            {
                //isChaseShop = false;
                ChaseLogic(playerTrasnform);
            }
            else
            {
                //isChaseShop = true;
                ChaseLogic(houseTransform);
            }
        }

        private void CheackAttackDistance()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("House") || collider.CompareTag("Player"))
                {
                    Attack();
                }
            }
        }

        protected virtual void Attack()
        {
            currentCoolTime += Time.deltaTime;
            Debug.Log("쿨타임시작!");

            if (currentCoolTime >= attackCoolTime)
            {
                isAttack = true;
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
            Collider2D collider = Physics2D.OverlapCircle(transform.position, range, chaseTarget);

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

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.gameObject.GetComponent<PlayerManager>().TakeDamage(bodyDamage);
                Debug.Log("실행");
            }
        }
    }
}