namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using Unity.VisualScripting;
    using UnityEditorInternal;

    //UnityEngine
    using UnityEngine;

    public class Slime : Monster
    {
        [SerializeField]
        private float attackDistance = default;
        [SerializeField]
        private float attackCoolTime = default;
        [SerializeField]
        private float jumpPower = default;

        public bool isAttack = false;
        public float currentCoolTime = default;


        

        protected override void OnEnable()
        {
            base.OnEnable();
            currentCoolTime = 0f;
        }

        protected override void Update()
        {
            base.Update();
            CheackAttackDistance();
        }

        private void CheackAttackDistance()
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackDistance);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Shop") || collider.CompareTag("Player"))
                {
                    isAttack = true;
                    Debug.Log("감지!");
                    Attack();
                }
            }
        }
        private void Attack()
        {
            currentCoolTime += Time.deltaTime;
            if(currentCoolTime >= attackCoolTime)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                Debug.Log("점프!");
                currentCoolTime = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Shop") && isAttack == true)
            {
                collision.gameObject.GetComponent<Shop>().TakeDamage(damage);
                Debug.Log("건물 공격!");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}


