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

        [Header("CharactorInfo")]
        public bool isDead = default;


        protected new SpriteRenderer renderer = null;
        protected Material originMaterial = null;
        public float currentHp = default;
        public float maxHp = default;

        private const float duration = 0.05f;

        private void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
            originMaterial = renderer.material;

            currentHp = maxHp;
        }

        public virtual void TakeDamage(float damage)
        {
            currentHp -= damage;

            SpriteFlash();

            if ((!isDead && currentHp <= 0))
            {
                Debug.Log(damage + "공격력");
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