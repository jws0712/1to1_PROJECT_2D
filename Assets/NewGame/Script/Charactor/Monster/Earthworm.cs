namespace OTO.Charactor.Monster
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class Earthworm : Monster
    {

        [Header("Info")]
        [SerializeField] private float hp = default;


        protected override void OnEnable()
        {
            base.OnEnable();
            maxHp = hp;
        }

        protected override void Update()
        {
            base.Update();
        }

        private void FixedUpdate()
        {
            
        }

    }

}

