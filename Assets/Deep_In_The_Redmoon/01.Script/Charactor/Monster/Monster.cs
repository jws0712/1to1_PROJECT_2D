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

    /// <summary>
    /// 몬스터의 이벤트들을 관리하는 부모 클래스
    /// </summary>
    public class Monster : Charactor
    {
        [Header("MonsterInfo")]
        [SerializeField] private MonsterData monsterData = null;
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float chaseRange = default;
        [SerializeField] private float attackRange = default;
        [SerializeField] private float stopDistance = default;
        [SerializeField] private float attackCoolTime = default;
        [SerializeField] protected float monsterSacle = default;
        [SerializeField] private LayerMask chaseTarget = default;

        [Header("DropItem")]
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
        protected bool isFlip = false;
        protected bool chaseHouse = false;

        /// <summary>
        /// 초기화
        /// </summary>
        protected virtual void OnEnable()
        {
            Init();
            if(GameObject.FindGameObjectWithTag("House") != null)
            {
                houseTransform = GameObject.FindGameObjectWithTag("House").transform;
            }
            
            currentCoolTime = 0f;
        }

        /// <summary>
        /// 플레이어를 추격하는 코드
        /// </summary>
        protected virtual void Update()
        {

            CheckRange(chaseRange);
            CheackAttackDistance();

            if (isChasePlayer == true)
            {
                ChaseLogic(playerTrasnform);
            }
            else if(chaseHouse == true)
            {
                ChaseLogic(houseTransform);
            }
        }
        /// <summary>
        /// 몬스터 클래스의 필드를 초기화 하는 함수
        /// </summary>
        private void Init()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();

            bulletDamage = monsterData.BulletDamage;
            bodyDamage = monsterData.BodyDamage;
            maxHp = monsterData.MaxHp;
        }

        /// <summary>
        /// 집과 플레이어가 공격거리에 있는지 검사하는 함수
        /// </summary>
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

        /// <summary>
        /// 공격 쿨타임이 다 돌면 공격을 실행하는 함수
        /// </summary>
        protected virtual void Attack()
        {
            currentCoolTime += Time.deltaTime;

            if (currentCoolTime >= attackCoolTime)
            {
                isAttack = true;
            }
        }

        /// <summary>
        /// 사정거리 내에 플레이어가 있는지 판단하는 함수
        /// </summary>
        /// <param name="range">자정거리</param>
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

        /// <summary>
        /// 타겟으로 일정거리 다가가면 멈추는 함수
        /// </summary>
        /// <param name="chaseTransform">타겟</param>
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

        /// <summary>
        /// 타겟을 추적하는 함수
        /// </summary>
        /// <param name="chaseTransform">타겟</param>
        private void Chase(Transform chaseTransform)
        {
            if (transform.position.x < chaseTransform.position.x)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(-monsterSacle, monsterSacle);
                isFlip = false;
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(monsterSacle, monsterSacle);
                isFlip = true;
            }
        }
        
        /// <summary>
        /// 몬스터를 멈추는 함수
        /// </summary>
        protected virtual void StopChase()
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        /// <summary>
        /// 몬스터가 죽었을때 실행되는 함수
        /// </summary>
        protected override void Die()
        {
            base.Die();

            GameManager.instance.fieldMonsterCount -= 1;

            DropItem(coinDiamond);

            Destroy(gameObject);


        }

        /// <summary>
        /// 몬스터가 죽었을때 아이템을 떨어뜨리는 함수
        /// </summary>
        /// <param name="dropItem">떨어질 아이템</param>
        private void DropItem(params GameObject[] dropItem)
        {
            for(int i = 0; i < dropItem.Length; i++)
            {
                GameObject item = Instantiate(dropItem[i], transform.position, Quaternion.identity);

                item.GetComponent<Rigidbody2D>().AddForce(Vector2.one * -2f, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// 데미지를 받을때 실행되는 함수
        /// </summary>
        /// <param name="damage">받을 데미지</param>
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        /// <summary>
        /// 플레이어와 부딛쳤을때 플레이어에게 피해를 입힘
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.gameObject.GetComponent<PlayerManager>().TakeDamage(bodyDamage);
            }
        }

        /// <summary>
        /// 플레이어와 부딛쳤을때 플레이어에게 피해를 입힘
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerManager>().TakeDamage(bodyDamage);
            }
        }

        /// <summary>
        /// 공격거리와 추격거리를 보여줌
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

    }
}