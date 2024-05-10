namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class Slime : Monster
    {
        [Header("Move")]
        [SerializeField] private float MoveSpeed;
        private void OnEnable()
        {
            base.Start();
            base.MonsterMovement();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            rb.velocity = new Vector2(MonsterBehavior * MoveSpeed, rb.velocity.y);
        }
    }
}


