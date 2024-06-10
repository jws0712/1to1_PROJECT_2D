namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using UnityEditorInternal;

    //UnityEngine
    using UnityEngine;

    public class Slime : Monster
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


