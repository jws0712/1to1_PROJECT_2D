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




        //private variables
        protected void Init()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
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
            if (IsDead) yield break; //Dead 상태라면 코루틴을 멈춤
            MonsterBehavior = Random.Range(-1, 2); //-1, 0, 1 사이의 값을 랜덤으로 MonsterBehavior에 할당함
            yield return new WaitForSeconds(1.2f); //1.2초 동안 대기함
            StartCoroutine(Co_SelectMovement()); //다시 코루틴을 호출해서 무한반복함
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
            //플래이어를 감지하는 코드를 구현
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.left, 30f, LayerMask.GetMask("Player"));
            Debug.DrawRay(rb.position, Vector3.left, new Color(0, 1, 0));
            if(rayHit.collider != null)
            {
                return;
            }
        }

        protected virtual void FlatformCheck(Vector2 direction ,float distance, float rayPos, LayerMask checkLayer)
        {
            Vector2 frontVec = new Vector2(rb.position.x + MonsterBehavior * rayPos, rb.position.y);
            Debug.DrawRay(frontVec, direction, new Color(0, 1, 0));
            rayHit = Physics2D.Raycast(frontVec, direction, distance, checkLayer);
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