namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using UnityEditor.Tilemaps;

    //UnityEngine
    using UnityEngine;

    public class Slime : Monster
    {
        [Header("Move")]
        [SerializeField] private float MoveSpeed;

        private float SpawnPower = 12f;
        private void OnEnable()
        {
            Init();
            rb.AddForce(Vector2.up * SpawnPower, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * SpawnPower, ForceMode2D.Impulse);

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

        protected override void FlipX()
        {
            base.FlipX();
        }

        protected override int MonsterMovement()
        {
            return base.MonsterMovement();
        }
    }
}


