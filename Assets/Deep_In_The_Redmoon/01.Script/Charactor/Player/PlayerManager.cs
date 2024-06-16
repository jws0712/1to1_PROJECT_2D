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
        [SerializeField] private float playerFlashNumber = default;
        [SerializeField] private float duration = default;


        [Header("CameraShake")]
        [SerializeField] private float shakePower = default;

        //Private variables
        private GameObject gunObject = null;
        private GameObject handPos = null;
        private SpriteRenderer gunRenderer = null;
        private LayerMask monsterLayer = default;
        private LayerMask playerLayer = default;
        private float maxCursePoint = default;
        private float currentCursePoint = default;

        private void Update()
        {
            GameManager.instance.hpSlider.value = currentHp/maxHp;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (isDead)
            {
                StopAllCoroutines();
            }

            StartCoroutine(Co_CameraShake(shakePower));

            handPos = GameObject.FindWithTag("HandPos");

            gunObject = handPos.transform.GetChild(0).gameObject;
            

            if (gunObject != null)
            {
                gunRenderer = gunObject.GetComponentInChildren<SpriteRenderer>();
            }

            StartCoroutine(PlayerSpriteFlash(playerFlashNumber));
        }

        private IEnumerator PlayerSpriteFlash(float number)
        {
            monsterLayer = LayerMask.NameToLayer("Monster");
            playerLayer = LayerMask.NameToLayer("Player");

            Color _playerAlhpa = renderer.color;

            for(int i = 0; i < number; i++)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, true);
                yield return new WaitForSeconds(0.05f);
                _playerAlhpa.a = 0f;
                renderer.color = _playerAlhpa;
                gunRenderer.color = _playerAlhpa;
                yield return new WaitForSeconds(duration);
                _playerAlhpa.a = 1f;
                renderer.color = _playerAlhpa;
                gunRenderer.color = _playerAlhpa;
                yield return new WaitForSeconds(duration);
            }
            Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, false);
        }

        private IEnumerator Co_CameraShake(float ShakeIntensity)
        {
            CameraShakeManager.instance.ShakeCamera(ShakeIntensity);
            yield return new WaitForSeconds(0.5f);
            CameraShakeManager.instance.StopShake();
        }

        protected override void Die()
        {
            base.Die();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Coin"))
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
