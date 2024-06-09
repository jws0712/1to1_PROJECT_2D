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
        [SerializeField] private float rayDistance = default;
        [SerializeField] private float rayPos = default;
        //[SerializeField] private Vector2 rayDirection = default;
        //[SerializeField] private LayerMask layerMask = default;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Update()
        {
            base.Update();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            rb.velocity = new Vector2(MonsterBehavior * moveSpeed, rb.velocity.y);
        }
    }

}

