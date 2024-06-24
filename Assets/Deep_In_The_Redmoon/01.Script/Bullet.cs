namespace OTO.Bullet
{
    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Charactor.Monster;
    using OTO.Charactor.Player;

    public class Bullet : MonoBehaviour
    {
        [Header("Bullet")]
        [SerializeField] private float BulletSpeed = default;
        [SerializeField] private float BulletDestroyTime = default;
        [SerializeField] private GameObject hitEffect = default;

        [HideInInspector]
        public float bulletDamage = default;

        void Start()
        {
            Invoke("BulletDestory", BulletDestroyTime);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * BulletSpeed * Time.fixedDeltaTime);
        }

        private void BulletDestory()
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            DestroyObject();

        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("Monster") || collision.CompareTag("Player"))
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
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

