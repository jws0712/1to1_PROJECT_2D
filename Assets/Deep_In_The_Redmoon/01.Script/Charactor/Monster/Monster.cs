namespace OTO.Charactor.Monster
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Charactor.Player;
    using OTO.Manager;

    public class Monster : Charactor
    {
        [Header("MonsterInfo")]
        [SerializeField] private MonsterData monsterData = null;
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float chaseRange = default;
        [SerializeField] private float attackRange = default;
        [SerializeField] private float stopDistance = default;
        [SerializeField] private float attackCoolTime = default;
        [SerializeField] private float monsterSacle = default;
        [SerializeField] private LayerMask chaseTarget = default;

        [Header("DropItem")]
        [SerializeField] private GameObject expDiamond = null;
        [SerializeField] private GameObject coinDiamond = null;



        //Protected variables
        protected Animator anim = null;
        protected Rigidbody2D rb = null;
        protected Transform playerTrasnform = null;
        protected Transform houseTransform = null;
        protected bool isChasePlayer = false;
        protected bool isAttack = false;
        protected float currentCoolTime = default;
        protected float bulletDamage = default;
        protected float bodyDamage = default;

        //Private variables


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
                ChaseLogic(playerTrasnform);
            }
            else
            {
                ChaseLogic(houseTransform);
            }
        }

        private void CheackAttackDistance()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player") && isChasePlayer == true)
                {
                    Attack();
                }
                else if(collider.CompareTag("House") && isChasePlayer == false)
                {
                    Attack();
                }
            }
        }

        protected virtual void Attack()
        {
            currentCoolTime += Time.deltaTime;

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

            bulletDamage = monsterData.BulletDamage;
            bodyDamage = monsterData.BodyDamage;
            maxHp = monsterData.MaxHp;
        }

        protected override void Die()
        {
            base.Die();

            GameManager.instance.fieldMonsterCount -= 1;

            DropItem(expDiamond, coinDiamond);

            Destroy(gameObject);
        }

        private void DropItem(params GameObject[] dropItem)
        {
            GameObject exp = Instantiate(dropItem[0], transform.position, Quaternion.identity);

            exp.GetComponent<Rigidbody2D>().AddForce(Vector2.one * 2f, ForceMode2D.Impulse);

            GameObject coin = Instantiate(dropItem[1], transform.position, Quaternion.identity);

            coin.GetComponent<Rigidbody2D>().AddForce(Vector2.one * -2f, ForceMode2D.Impulse);
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
                Debug.Log("½ÇÇà");
            }
        }
    }
}