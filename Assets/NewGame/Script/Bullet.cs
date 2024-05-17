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

        //private variables

        void Start()
        {
            Invoke("DestoryBullet", BulletDestroyTime);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.Translate(Vector2.right * BulletSpeed * Time.deltaTime);
        }

        private void DestoryBullet()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("Monster"))
            {
                DestoryBullet();
            }
            if (collision.CompareTag("Monster"))
            {
                collision.GetComponent<Monster>().TakeDamage(1f);
                
            }
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHp>().TakeDamage(1f);
            }
        }
    }
}

