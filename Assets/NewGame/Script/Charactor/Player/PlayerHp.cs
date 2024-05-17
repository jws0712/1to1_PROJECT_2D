namespace OTO.Charactor.Player
{

    //System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    public class PlayerHp : Charactor
    {
        [Header("Flash")]
        [SerializeField] private float playerFlashNumber = default;
        [SerializeField] private float duration = default;

        private GameObject gunObject = null;
        private SpriteRenderer gunRenderer = null;
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            gunObject = GameObject.FindWithTag("Gun");

            StartCoroutine(PlayerSpriteFlash(playerFlashNumber));
        }

        private IEnumerator PlayerSpriteFlash(float number)
        {
            Color _playerAlhpa = renderer.color;

            for(int i = 0; i < number; i++)
            {
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


        }

        protected override void Die()
        {
            base.Die();

            Debug.Log("플레이어 사망");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                TakeDamage(1f);
            }
        }
    }
}
