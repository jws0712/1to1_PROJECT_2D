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

        public float damage = default;

        protected virtual void OnEnable()
        {
            Init();
            MonsterMovement();
        }
        protected virtual void Update()
        {
            FlipX();
        }
        /// <summary>
        /// 컴포넌트를 초기화하는 함수
        /// </summary>
        private void Init()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }
        /// <summary>
        /// Behavior값을 return해주는 함수
        /// </summary>
        /// <returns></returns>
        private int MonsterMovement()
        {
            StartCoroutine(Co_SelectMovement());
            return MonsterBehavior;
        }
        private IEnumerator Co_SelectMovement()
        {
            MonsterBehavior = Random.Range(-1, 2);
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(Co_SelectMovement());
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
        /// 대미지를 받을때 실행되는 함수
        /// </summary>
        /// <param name="damage"></param>
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        /// <summary>
        /// 죽을때 실행되는 함수
        /// </summary>
        protected override void Die()
        {
            base.Die();

            Destroy(gameObject);
        }
    }
}