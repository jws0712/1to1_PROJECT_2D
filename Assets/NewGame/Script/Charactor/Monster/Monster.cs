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
        protected Rigidbody2D rb = null;
        protected Animator anim = null;
        protected new SpriteRenderer renderer = null;


        //private variables

        //public variables
        public float damage = default;




        /// <summary>
        /// ������Ʈ�� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        protected void Init()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
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

        protected virtual void FlipX()
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

        protected virtual void FindPlayer()
        {
            //�÷��̾ �����ϴ� �ڵ带 ����
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.left, 30f, LayerMask.GetMask("Player"));
            Debug.DrawRay(rb.position, Vector3.left, new Color(0, 1, 0));
            if(rayHit.collider != null)
            {
                return;
            }
        }

        /// <summary>
        /// �÷����� üũ�ϴ� �Լ�
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <param name="rayPos"></param>
        /// <param name="checkLayer"></param>
        protected virtual void CheckGround(Vector2 direction ,float distance, float rayPos, LayerMask checkLayer)
        {
            Vector2 frontVec = new Vector2(rb.position.x + MonsterBehavior * rayPos, rb.position.y);
            Debug.DrawRay(frontVec, direction, new Color(0, 1, 0));
            rayHit = Physics2D.Raycast(frontVec, direction, distance, checkLayer);
        }

        protected virtual void DropEx()
        {
            //����ġ�� ����ϴ� �ڵ带 ����
        }

        protected virtual void DropCoin()
        {
            //���� ����ϴ� �ڵ带 ����
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            Debug.Log("�� ���Ͱ� �¾Ҵ�!");
        }

        protected override void Die()
        {
            base.Die();

            Destroy(gameObject);
            Debug.Log("�� �׾���");
        }
    }
}