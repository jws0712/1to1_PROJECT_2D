namespace OTO.Charactor.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Charactor.Monster;
    using OTO.Manager;

    public class PlayerManager : Charactor
    {
        [Header("GetHitFlash")]
        [SerializeField] private float playerFlashCount = default;
        [SerializeField] private float duration = default;

        //Private variables
        private GameObject gunObject = null;
        private GameObject handPos = null;
        private SpriteRenderer gunRenderer = null;
        private LayerMask monsterLayer = default;
        private LayerMask playerLayer = default;

        protected override void Start()
        {
            base.Start();
            monsterLayer = LayerMask.NameToLayer("Monster");
            playerLayer = LayerMask.NameToLayer("Player");

            Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, false);
        }

        private void Update()
        {
            GameManager.instance.hpSlider.value = currentHp / maxHp;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            handPos = GameObject.FindWithTag("HandPos");

            if(handPos != null)
            {
                gunObject = handPos.transform.GetChild(0).gameObject;
            }

            if (gunObject != null)
            {
                gunRenderer = gunObject.GetComponentInChildren<SpriteRenderer>();
            }

            StartCoroutine(Co_PlayerSpriteFlash(playerFlashCount));
        }

        private IEnumerator Co_PlayerSpriteFlash(float Count)
        {


            Color _playerAlpha = renderer.color;
            for (int i = 0; i < Count; i++)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, true);
                yield return new WaitForSeconds(0.05f);
                _playerAlpha.a = 0f;
                renderer.color = _playerAlpha;
                gunRenderer.color = _playerAlpha;
                yield return new WaitForSeconds(duration);
                _playerAlpha.a = 1f;
                renderer.color = _playerAlpha;
                gunRenderer.color = _playerAlpha;
                yield return new WaitForSeconds(duration);
            }
            Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, false);
        }
        protected override void Die()
        {
            base.Die();
            GameManager.instance.hpSlider.value = 0f;
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Coin"))
            {
                GameManager.instance.GetCoin();
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("CursePoint"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
