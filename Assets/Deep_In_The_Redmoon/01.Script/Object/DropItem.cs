namespace OTO.Object
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// 몬스터 에게서 떨어지는 아이템의 기능을 구현한 클래스
    /// </summary>
    public class DropItem : MonoBehaviour
    {
        private Rigidbody2D rb = null;

        /// <summary>
        /// 컴포넌트 초기화
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// 땅에 떨어졌을때 미끄러지지 않게 하는 코드
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
