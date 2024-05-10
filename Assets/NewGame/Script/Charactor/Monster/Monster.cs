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
            //오른쪽, 왼쪽, 아이들 움직임 구현
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
            //플래이어를 감지하는 코드를 구현
        }

        protected virtual void DropEx()
        {
            //경험치를 드롭하는 코드를 구현
        }

        protected virtual void DropCoin()
        {
            //돈을 드롭하는 코드를 구현
        }
    }
}