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
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private LayerMask layerMask = default;
        [SerializeField] private float rayDistance = default;
        [SerializeField] private float rayPos = default;
        [SerializeField] private Vector2 rayDirection = default;

        private void OnEnable()
        {
            Init();
            MonsterMovement();
        }

        private void Update()
        {
            FlipX();
            FindPlayer();
            FlatformCheck(rayDirection ,rayDistance, rayPos, layerMask);
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            rb.velocity = new Vector2(MonsterBehavior * moveSpeed, rb.velocity.y);
        }

        protected override void FlipX()
        {
            base.FlipX();
        }

        protected override int MonsterMovement()
        {
            return base.MonsterMovement();
        }

        protected override void FindPlayer()
        {
            base.FindPlayer();
        }

        protected override void FlatformCheck(Vector2 direction, float distance, float rayPos, LayerMask layerMask)
        {
            base.FlatformCheck(direction, distance, rayPos, layerMask);
            if (rayHit.collider == null)
            {
                Debug.Log("∂•æ»¥Í¿Ω");
                MonsterBehavior *= -1;
            }
        }
        

            
        
    }

}

