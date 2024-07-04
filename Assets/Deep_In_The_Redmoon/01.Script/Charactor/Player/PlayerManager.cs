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

    /// <summary>
    /// 플레이어의 상태를 관리하는 클래스
    /// </summary>
    public class PlayerManager : Charactor
    {
        [Header("GetHit")]
        [SerializeField] private float playerFlashCount = default;
        [SerializeField] private float duration = default;

        //private variables
        private GameObject gunObject = null;
        private GameObject handPos = null;
        private SpriteRenderer gunRenderer = null;
        private PlayerMovement playerMovement = null;
        private Rigidbody2D rb = null;
        private LayerMask monsterLayer = default;
        private LayerMask playerLayer = default;

        /// <summary>
        /// 캐릭터 클래스의 스타트를 실행시키고 변수들을 초기화함
        /// </summary>
        protected override void Start()
        {
            base.Start();
            monsterLayer = LayerMask.NameToLayer("Monster");
            playerLayer = LayerMask.NameToLayer("Player");

            Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, false);

            playerMovement = GetComponent<PlayerMovement>();
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// 플레이어의 체력과 체력 UI를 관리함
        /// </summary>
        private void Update()
        {
            GameManager.instance.hpSlider.value = currentHp / maxHp;

            if(GameManager.instance.isFieldClear == true)
            {
                currentHp = maxHp;
            }
        }

        /// <summary>
        /// 데미지를 받을때 실행하는 함수
        /// </summary>
        /// <param name="damage">받을 데미지</param>
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            CameraShakeManager.instance.PlayShake("Hit");

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

        /// <summary>
        /// 플레이어가 데미지를 받았을때 플레이어 오브젝트를 깜빡거리게 만든 코루틴
        /// </summary>
        /// <param name="Count"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 플레이어가 죽었을때 실행하는 함수
        /// </summary>
        protected override void Die()
        {
            base.Die();
            GameManager.instance.hpSlider.value = 0f;
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }

        /// <summary>
        /// 아이템과 부딛쳤을때를 실행되는 코드
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Coin"))
            {
                AudioManager.instance.PlaySFX("GetCoin");
                GameManager.instance.GetCoin();
                Destroy(collision.gameObject);
            }
        }
    }
}
