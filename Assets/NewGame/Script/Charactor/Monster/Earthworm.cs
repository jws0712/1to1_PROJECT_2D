namespace OTO.Charactor.Monster
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class Earthworm : Monster
    {
        [Header("BulletInfo")]
        [SerializeField]
        private GameObject bulletObject = null;
        [SerializeField]
        private Transform firePos = null;



        private float rotZ = default;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Update()
        {
            base.Update();

            if(isChasePlayer == true)
            {
                GetFireRot(playerTrasnform);
            }
            else
            {
                GetFireRot(houseTransform);
            }
        }

        protected override void Attack()
        {
            base.Attack();
            if (isAttack == true)
            {
                Quaternion bulletAngle = Quaternion.Euler(0, 0, rotZ);
                Instantiate(bulletObject, firePos.position, bulletAngle);
                isAttack = false;
                currentCoolTime = 0;
            }
        }

        private void GetFireRot(Transform targetPos)
        {
            Vector3 rotation = targetPos.position - firePos.position;

            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }

    }

}

