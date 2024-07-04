namespace OTO.Object
{
    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Charactor.Monster;
    using OTO.Charactor.Player;

    /// <summary>
    /// 총알의 기능을 구현한 클래스
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet")]
        [SerializeField] public float BulletSpeed = default;
        [SerializeField] private float BulletDestroyTime = default;
        [SerializeField] private GameObject hitEffect = default;

        [HideInInspector]
        public float bulletDamage = default;

        /// <summary>
        /// 총알이 소환되고 일정시간뒤에 파괴함
        /// </summary>
        void Start()
        {
            Invoke("BulletDestory", BulletDestroyTime);
        }

        /// <summary>
        /// 총알을 움직이는 코드
        /// </summary>
        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * BulletSpeed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// 총알이 파괴될때 실행되는 함수
        /// </summary>
        private void BulletDestory()
        {
            if(hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
            DestroyObject();

        }

        /// <summary>
        /// 총알이 파괴되는 함수
        /// </summary>
        private void DestroyObject()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 총알이 특정 테그와 부딛쳤을때 실행되는 코드
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("Monster") || collision.CompareTag("Player"))
            {
                if(hitEffect != null)
                {
                    Instantiate(hitEffect, transform.position, transform.rotation);
                }
                
                DestroyObject();
            }

            if(gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                if (collision.CompareTag("House"))
                {
                    collision.GetComponent<Shop>().TakeDamage(bulletDamage);
                    Instantiate(hitEffect, transform.position, transform.rotation);
                }
            }

            if (collision.CompareTag("Monster"))
            {
                collision.GetComponent<Monster>().TakeDamage(bulletDamage);
            }
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerManager>().TakeDamage(bulletDamage);
            }
        }
    }
}

