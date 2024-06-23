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

    //Project
    using OTO.Bullet;

    /// <summary>
    /// 박쥐 몬스터의 행동 이벤트를 관리하는 클래스
    /// </summary>
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
            chaseHouse = true;

            base.OnEnable();
        }

        protected override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 몬스터 공격을 실행하는 함수
        /// </summary>
        protected override void Attack()
        {
            base.Attack();
            if (isAttack == true)
            {
                float bulletSpread = transform.rotation.z + startBulletSpreadAngle;
                for (int i = 0; i < bulletNumber; i++)
                {
                    Quaternion bulletAngle = Quaternion.Euler(0, 0, bulletSpread);
                    GameObject _bullet = Instantiate(bulletObject, transform.position, bulletAngle);
                    _bullet.GetComponent<Bullet>().bulletDamage = bulletDamage;
                    bulletSpread -= bulletSpeadAngle;

                }
                bulletSpread = transform.rotation.z + startBulletSpreadAngle * 2;

                isAttack = false;
                currentCoolTime = 0;
            }
        }
    }
}


