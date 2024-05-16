namespace OTO.Charactor
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.Experimental.GlobalIllumination;

    public class Charactor : MonoBehaviour
    {
        public bool isDead = default;
        public float maxHp = default;
        public float startHp = default;

        private void Start()
        {
            maxHp = startHp;
        }

        //데미지를 받았을때 처리할 매서드
        public virtual void TakeDamage(float damage)
        {
            maxHp -= damage;

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
    }
}