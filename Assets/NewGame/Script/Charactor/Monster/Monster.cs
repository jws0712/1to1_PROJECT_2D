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
        //Protected variables
        protected int MonsterBehavior = default;
        protected RaycastHit2D rayHit = default;
        protected Animator anim = null;
        protected Rigidbody2D rb;
        protected GameObject expDiamond = null;
        protected GameObject coinDiamond = null;

        //private variables

        //public variables
        public float damage = default;

        protected virtual void OnEnable()
        {
            Init();
        }

        protected virtual void Update()
        {
            FlipX();
        }

        /// <summary>
        /// ������Ʈ�� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        private void Init()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Behavior���� return���ִ� �Լ�
        /// </summary>
        /// <returns></returns>
        protected virtual int MonsterMovement()
        {
            StartCoroutine(Co_SelectMovement());
            return MonsterBehavior;
        }

        // Ư���ð����� -1, 0, 1 �߿��� �Ѱ��� ���� Behavior������ �־��ִ� �ڷ�ƾ
        private IEnumerator Co_SelectMovement()
        {
            MonsterBehavior = Random.Range(-1, 2); //-1, 0, 1 ������ ���� �������� MonsterBehavior�� �Ҵ���
            yield return new WaitForSeconds(1.2f); //1.2�� ���� �����
            StartCoroutine(Co_SelectMovement()); //�ٽ� �ڷ�ƾ�� ȣ���ؼ� ���ѹݺ���
        }

        private void FlipX()
        {
            if (MonsterBehavior == -1)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (MonsterBehavior == 1)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }        

        /// <summary>
        /// ������� ������ ����Ǵ� �Լ�
        /// </summary>
        /// <param name="damage"></param>
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        /// <summary>
        /// ������ ����Ǵ� �Լ�
        /// </summary>
        protected override void Die()
        {
            base.Die();

            Destroy(gameObject);
        }
    }
}