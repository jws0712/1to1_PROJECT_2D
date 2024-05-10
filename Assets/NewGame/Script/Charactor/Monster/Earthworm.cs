namespace OTO.Charactor.Monster
{


    //System
    using System.Collections;
    using System.Collections.Generic;
    

    //UnityEngine
    using UnityEngine;

    public class Earthworm : Monster
    {
        [Header("Move")]
        [SerializeField] private float MoveSpeed;

        private float SpawnPower = 12f;
        private void OnEnable()
        {
            Init();

            MonsterMovement();
        }

        private void Update()
        {
            FlipX();
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

