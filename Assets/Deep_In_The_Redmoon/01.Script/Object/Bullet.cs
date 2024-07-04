namespace OTO.Object
{
    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Charactor.Monster;
    using OTO.Charactor.Player;

    /// <summary>
    /// �Ѿ��� ����� ������ Ŭ����
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet")]
        [SerializeField] private float BulletSpeed = default;
        [SerializeField] private float BulletDestroyTime = default;
        [SerializeField] private GameObject hitEffect = default;

        [HideInInspector]
        public float bulletDamage = default;

        /// <summary>
        /// �Ѿ��� ��ȯ�ǰ� �����ð��ڿ� �ı���
        /// </summary>
        void Start()
        {
            Invoke("BulletDestory", BulletDestroyTime);
        }

        /// <summary>
        /// �Ѿ��� �����̴� �ڵ�
        /// </summary>
        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * BulletSpeed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// �Ѿ��� �ı��ɶ� ����Ǵ� �Լ�
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
        /// �Ѿ��� �ı��Ǵ� �Լ�
        /// </summary>
        private void DestroyObject()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// �Ѿ��� Ư�� �ױ׿� �ε������� ����Ǵ� �ڵ�
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

