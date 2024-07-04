namespace OTO.Charactor.Monster
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    //Project
    using OTO.Object;
    using OTO.Manager;

    /// <summary>
    /// 지렁이의 행동을 관리하는 클래스
    /// </summary>
    public class Earthworm : Monster
    {
        [Header("BulletInfo")]
        [SerializeField]
        private GameObject bulletObject = null;
        [SerializeField]
        private Transform firePos = null;
        [SerializeField]
        private int bulletNumber = default;
        [SerializeField]
        private int bulletSpreadAngle = default;
        [SerializeField]
        private int startBulletSpreadAngle = default;


        private float rotZ = default;

        /// <summary>
        /// 초기화
        /// </summary>
        protected override void OnEnable()
        {
            chaseHouse = true; //집을 추격함

            base.OnEnable();
        }

        /// <summary>
        /// 몬스터 클래스의 업데이트를 실행하고 플레이어에게 발사할 총알의 각도를 구함
        /// </summary>
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

        /// <summary>
        /// 지렁이의 공격을 실행하는 함수
        /// </summary>
        protected override void Attack()
        {
            base.Attack();
            if (isAttack == true)
            {
                float bulletSpread = rotZ + startBulletSpreadAngle;
                AudioManager.instance.PlaySFX("EarthWormAttack");
                for(int i = 0; i < bulletNumber; i++)
                {
                    Quaternion bulletAngle = Quaternion.Euler(0, 0, bulletSpread);
                    GameObject _bullet = Instantiate(bulletObject, firePos.position, bulletAngle);
                    _bullet.GetComponent<Bullet>().bulletDamage = bulletDamage;
                    bulletSpread -= bulletSpreadAngle;

                }
                bulletSpread = rotZ + startBulletSpreadAngle * 2;
                

                isAttack = false;
                currentCoolTime = 0;
            }
        }

        /// <summary>
        /// 타겟의 위치를 가져와서 총알 발사 각도를 구하는 함수
        /// </summary>
        /// <param name="targetPos"></param>
        private void GetFireRot(Transform targetPos)
        {
            Vector3 rotation = targetPos.position - firePos.position;

            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }

    }

}

