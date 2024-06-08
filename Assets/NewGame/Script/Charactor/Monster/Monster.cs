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
        protected virtual int MonsterMovement()
        {
            StartCoroutine(Co_SelectMovement());
            return MonsterBehavior;
        }

        // 특정시간마다 -1, 0, 1 중에서 한게의 값을 Behavior변수에 넣어주는 코루틴
        private IEnumerator Co_SelectMovement()
        {
            MonsterBehavior = Random.Range(-1, 2); //-1, 0, 1 사이의 값을 랜덤으로 MonsterBehavior에 할당함
            yield return new WaitForSeconds(1.2f); //1.2초 동안 대기함
            StartCoroutine(Co_SelectMovement()); //다시 코루틴을 호출해서 무한반복함
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