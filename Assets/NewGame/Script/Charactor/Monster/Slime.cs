namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class Slime : Monster
    {
        private void OnEnable()
        {
            base.Start();
            base.MonsterMovement();
        }
    }
}


