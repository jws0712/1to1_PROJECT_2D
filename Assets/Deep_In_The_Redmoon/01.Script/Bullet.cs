namespace OTO.Bullet
{
    //Microsoft
    using Microsoft.Unity.VisualStudio.Editor;

    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using Unity.VisualScripting;

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

        public float bulletDamage = default;

        void Start()
        {
            Co_BulletDestory();
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * BulletSpeed * Time.fixedDeltaTime);
        }

        private IEnumerator Co_BulletDestory()
        {
            yield return new WaitForSeconds(BulletDestroyTime);

            Instantiate(hitEffect, transform.position, transform.rotation);
            DestoryBullet();

        }

        private void DestoryBullet()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("Monster") || collision.CompareTag("Player"))
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
                DestoryBullet();
            }

            if(gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                if (collision.CompareTag("House"))
                {
                    collision.GetComponent<Shop>().TakeDamage(bulletDamage); //몬스터
                    Instantiate(hitEffect, transform.position, transform.rotation);
                    DestoryBullet();
                }
            }


            if (collision.CompareTag("Monster"))
            {
                collision.GetComponent<Monster>().TakeDamage(bulletDamage); //플레이어
            }
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerManager>().TakeDamage(bulletDamage); //몬스터
            }
        }
    }
}

