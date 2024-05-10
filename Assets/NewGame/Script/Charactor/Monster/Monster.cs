namespace OTO.Charactor.Monster
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    public class Monster : MonoBehaviour
    {
        private Rigidbody2D rb;

        private bool IsDead;
        
        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        protected virtual void MonsterMovement()
        {
            //������, ����, ���̵� ������ ����
            StartCoroutine(Co_SelectMovement());
            
        }

        private IEnumerator Co_SelectMovement()
        {
            if (IsDead) yield break;
            int Behavior;
            float Speed = 15f;

            Behavior = Random.Range(-1, 2);

            switch (Behavior)
            {
                case 0:
                    rb.velocity = Vector2.zero;
                    break;
                case -1:
                    rb.velocity = Vector2.left * Speed;
                    break;
                case 1:
                    rb.velocity = Vector2.right * Speed;
                    break;
            }

            yield return new WaitForSeconds(0.8f);

            StartCoroutine(Co_SelectMovement());
        }

        protected virtual void FindPlayer()
        {
            //�÷��̾ �����ϴ� �ڵ带 ����
        }

        protected virtual void DropEx()
        {
            //����ġ�� ����ϴ� �ڵ带 ����
        }

        protected virtual void DropCoin()
        {
            //���� ����ϴ� �ڵ带 ����
        }
    }
}