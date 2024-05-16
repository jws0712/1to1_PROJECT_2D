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

        //�������� �޾����� ó���� �ż���
        public virtual void TakeDamage(float damage)
        {
            maxHp -= damage;

            if ((!isDead && maxHp <= 0))
            {
                Die();
            }
        }

        //��� ó��
        protected virtual void Die() 
        {
            isDead = true;
        }
    }
}