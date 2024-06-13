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

        void Start()
        {
            Invoke("DestoryBullet", BulletDestroyTime);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * BulletSpeed * Time.fixedDeltaTime);
        }

        private void DestoryBullet()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("Monster") || collision.CompareTag("Player"))
            {
                DestoryBullet();
            }

            if(gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                if (collision.CompareTag("House"))
                {
                    DestoryBullet();
                }
            }


            if (collision.CompareTag("Monster"))
            {
                collision.GetComponent<Monster>().TakeDamage(1f); //플레이어
            }
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerManager>().TakeDamage(1f); //몬스터
            }
            if (collision.CompareTag("House"))
            {
                collision.GetComponent<Shop>().TakeDamage(1f); //몬스터
            }
        }
    }
}

