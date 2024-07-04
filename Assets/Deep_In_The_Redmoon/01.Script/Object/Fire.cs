namespace OTO.Object
{
    using OTO.Charactor.Monster;
    using OTO.Charactor.Player;
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class Fire : MonoBehaviour
    {
        [Header("FireInfo")]
        [SerializeField] private float fireDamage = default;

        /// <summary>
        /// 불기둥이 파괴되는 함수
        /// </summary>
        private void DestroyObject()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 불기둥이 특정 테그와 부딛쳤을때 실행되는 코드
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerManager>().TakeDamage(fireDamage);
            }
        }
    }
}
