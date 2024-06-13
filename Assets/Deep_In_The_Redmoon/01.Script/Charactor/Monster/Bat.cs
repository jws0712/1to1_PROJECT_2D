namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using Unity.VisualScripting;
    using UnityEditorInternal;

    //UnityEngine
    using UnityEngine;

    public class Bat : Monster
    {
        [Header("BatInfo")]
        [SerializeField]
        private GameObject bulletObject = null;
        [SerializeField]
        private int bulletNumber = default;
        [SerializeField]
        private int bulletSpeadAngle = default;
        [SerializeField]
        private int startBulletSpreadAngle = default;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Attack()
        {
            base.Attack();
            if (isAttack == true)
            {
                float bulletSpread = transform.rotation.z + startBulletSpreadAngle;
                for (int i = 0; i < bulletNumber; i++)
                {
                    Quaternion bulletAngle = Quaternion.Euler(0, 0, bulletSpread);
                    Instantiate(bulletObject, transform.position, bulletAngle);
                    bulletSpread -= bulletSpeadAngle;

                }
                bulletSpread = transform.rotation.z + startBulletSpreadAngle * 2;


                isAttack = false;
                currentCoolTime = 0;
            }
        }
    }
}


