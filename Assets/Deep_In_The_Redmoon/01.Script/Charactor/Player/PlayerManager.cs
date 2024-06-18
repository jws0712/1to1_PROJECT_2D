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

        private Coroutine cameraShakeCoroutine = null; // 코루틴 참조 변수

        private void Update()
        {
            GameManager.instance.hpSlider.value = currentHp / maxHp;

            if (isDead && cameraShakeCoroutine != null)
            {
                StopCoroutine(cameraShakeCoroutine);
                cameraShakeCoroutine = null;
            }
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (isDead)
            {
                StopCoroutine(Co_PlayerSpriteFlash(playerFlashCount));
                if (cameraShakeCoroutine != null)
                {
                    StopCoroutine(cameraShakeCoroutine);
                    cameraShakeCoroutine = null;
                }
                Debug.Log("작동");
                return; // 사망 시 더 이상 실행하지 않도록 반환
            }

            if (cameraShakeCoroutine != null)
            {
                StopCoroutine(cameraShakeCoroutine);
            }

            handPos = GameObject.FindWithTag("HandPos");

            gunObject = handPos.transform.GetChild(0).gameObject;

            if (gunObject != null)
            {
                gunRenderer = gunObject.GetComponentInChildren<SpriteRenderer>();
            }

            StartCoroutine(Co_PlayerSpriteFlash(playerFlashCount));
        }

        private IEnumerator Co_PlayerSpriteFlash(float Count)
        {
            monsterLayer = LayerMask.NameToLayer("Monster");
            playerLayer = LayerMask.NameToLayer("Player");

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
            isDead = true; // 사망 상태 설정
            GameManager.instance.hpSlider.value = 0f;
            GameManager.instance.GameOver();
            gameObject.SetActive(false);
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
                GameManager.instance.GetCursePoint();
                Destroy(collision.gameObject);
            }
        }
    }
}
