namespace OTO.Charactor
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.Experimental.GlobalIllumination;

    public class Charactor : MonoBehaviour
    {
        [Header("Material")]
        [SerializeField] protected Material flashMaterial = null;

        public bool isDead = default;
        public float maxHp = default;
        public float startHp = default;

        protected new SpriteRenderer renderer = null;
        protected Material originMaterial = null;

        private const float duration = 0.05f;

        private void Start()
        {
            maxHp = startHp;

            renderer = GetComponent<SpriteRenderer>();

            originMaterial = renderer.material;
        }

        //데미지를 받았을때 처리할 매서드
        public virtual void TakeDamage(float damage)
        {
            maxHp -= damage;

            SpriteFlash();

            if ((!isDead && maxHp <= 0))
            {
                Die();
            }
        }

        //사망 처리
        protected virtual void Die() 
        {
            isDead = true;
        }

        private void SpriteFlash()
        {
            StartCoroutine(Co_SpriteFlash());
        }

        private IEnumerator Co_SpriteFlash()
        {
            renderer.material = flashMaterial;

            yield return new WaitForSeconds(duration);

            renderer.material = originMaterial;
        }
    }
}