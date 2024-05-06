namespace OTO.Charactor
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class Charactor : MonoBehaviour
    {
        public float MaxHp;
        public float CurrentHp { get; protected set; }
        public bool IsDie { get; protected set; }

        protected virtual void OnEnable()
        {
            IsDie = false;

            CurrentHp = MaxHp;
        }

        public virtual void GetDamage(float Damage)
        {
            CurrentHp -= Damage;

            if (CurrentHp <= 0 && !IsDie)
            {
                IsDie = true;
            }
        }
    }
}

