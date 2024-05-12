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

        private void OnEnable()
        {
            Init();
            MonsterMovement();
        }

        private void Update()
        {
            FlipX();
            PlatformCheck();
            FindPlayer();
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

        protected override void FindPlayer()
        {
            base.FindPlayer();
        }

        private void PlatformCheck()
        {
            Vector2 frontVec = new Vector2(rb.position.x + MonsterBehavior, rb.position.y);
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1.5f, LayerMask.GetMask("Ground"));
            if(rayHit.collider == null)
            {
                MonsterBehavior *= -1;
            }
        }
    }

}

