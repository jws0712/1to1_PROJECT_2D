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
    /// ������ �̺�Ʈ���� �����ϴ� �θ� Ŭ����
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
        [SerializeField] private float monsterSacle = default;
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
            else if(chaseHouse == true)
            {
                ChaseLogic(houseTransform);
            }
        }
        /// <summary>
        /// ���� Ŭ������ �ʵ带 �ʱ�ȭ �ϴ� �Լ�
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
        /// ���� �÷��̾ ���ݰŸ��� �ִ��� �˻��ϴ� �Լ�
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
        /// ���� ��Ÿ���� �� ���� ������ �����ϴ� �Լ�
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
        /// �����Ÿ� ���� �÷��̾ �ִ��� �Ǵ��ϴ� �Լ�
        /// </summary>
        /// <param name="range">�����Ÿ�</param>
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
        /// Ÿ������ �����Ÿ� �ٰ����� ���ߴ� �Լ�
        /// </summary>
        /// <param name="chaseTransform">Ÿ��</param>
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
        /// Ÿ���� �����ϴ� �Լ�
        /// </summary>
        /// <param name="chaseTransform">Ÿ��</param>
        private void Chase(Transform chaseTransform)
        {
            if (transform.position.x < chaseTransform.position.x && isAttack)
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
        /// ���͸� ���ߴ� �Լ�
        /// </summary>
        protected virtual void StopChase()
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        /// <summary>
        /// ���Ͱ� �׾����� ����Ǵ� �Լ�
        /// </summary>
        protected override void Die()
        {
            base.Die();

            GameManager.instance.fieldMonsterCount -= 1;

            DropItem(coinDiamond);

            Destroy(gameObject);


        }

        /// <summary>
        /// ���Ͱ� �׾����� �������� ����߸��� �Լ�
        /// </summary>
        /// <param name="dropItem">������ ������</param>
        private void DropItem(params GameObject[] dropItem)
        {
            for(int i = 0; i < dropItem.Length; i++)
            {
                GameObject item = Instantiate(dropItem[i], transform.position, Quaternion.identity);

                item.GetComponent<Rigidbody2D>().AddForce(Vector2.one * -2f, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// �������� ������ ����Ǵ� �Լ�
        /// </summary>
        /// <param name="damage">���� ������</param>
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.gameObject.GetComponent<PlayerManager>().TakeDamage(bodyDamage);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerManager>().TakeDamage(bodyDamage);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

    }
}