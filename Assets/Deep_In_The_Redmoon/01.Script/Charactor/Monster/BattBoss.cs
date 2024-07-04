namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;

    //UnityEngine
    using UnityEngine;

    public class BattBoss : Monster
    {   
        /// <summary>
        /// 보스의 상태
        /// </summary>
        private enum BatBossState
        {
            Idle,
            Skill1,
            Skill2,
            Skill3,
        }

        private BatBossState batBossState;

        [Header("BulletInfo")]
        [SerializeField]
        private GameObject bulletObject = null;
        [SerializeField]
        private Transform firePos = null;
        [SerializeField]
        private float bulletSpeed = default;

        private float rotZ = default;
        private Transform playerPos = default;

        /// <summary>
        /// 변수 초기화
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            currentCoolTime = 0f;

            batBossState = BatBossState.Idle;
            anim.SetTrigger("Smoke");
            
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Update()
        {
            base.Update();

            if(GameObject.FindGameObjectWithTag("Player") != null)
            {
                playerPos = GameObject.FindGameObjectWithTag("Player").transform;

                if (transform.position.x < playerPos.position.x)
                {
                    transform.localScale = new Vector2(-monsterSacle, monsterSacle);
                    isFlip = false;
                }
                else
                {
                    transform.localScale = new Vector2(monsterSacle, monsterSacle);
                    isFlip = true;
                }
            }



            rb.velocity = Vector2.down;
        }


        public void ChangeState()
        {

            switch(batBossState)
            {
                case BatBossState.Idle:
                    {
                        Debug.Log("스위치 아이들");
                        Idle();
                        break;
                    }
                case BatBossState.Skill1:
                    {
                        Skill1();
                        break;
                    }
                //case BatBossState.Skill2:
                //    {
                //        Skill2();
                //        break;
                //    }
                //case BatBossState.Skill3:
                //    {
                //        Skill3();
                //        break;
                //    }
            }
        }

        /// <summary>
        /// 기본 상태일때 실행되는 함수
        /// </summary>
        private void Idle()
        {
            transform.position = new Vector3(0, 0.3463205f, 0);
            StartCoroutine(Co_Idle());
        }

        private IEnumerator Co_Idle()
        {
            anim.SetTrigger("Idle");
            yield return new WaitForSeconds(3);
            int skillIndex = 1; //Random.Range(1, 4);

            switch (skillIndex)
            {
                case 1:
                    {
                        batBossState = BatBossState.Skill1;
                        
                        break;
                    }
                    //case 2:
                    //    {
                    //        batBossState = BatBossState.Skill2;
                    //        break;
                    //    }
                    //case 3:
                    //    {
                    //        batBossState = BatBossState.Skill3;
                    //        break;
                    //    }
            }

            anim.SetTrigger("Smoke");


        }

        /// <summary>
        /// 검기를 날리는 스킬을 구현한 함수
        /// </summary>
        private void Skill1()
        {
            StartCoroutine(Co_Skill1());
        }

        private IEnumerator Co_Skill1()
        {
            transform.position = new Vector2(playerPos.position.x + 15, 0.3463205f);

            anim.SetTrigger("Skill1");

            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1.5f);
                anim.SetTrigger("Skill1_Attack");
            }

            anim.ResetTrigger("Skill1_Idle");
            batBossState = BatBossState.Idle;
            anim.SetTrigger("Smoke");

        }

        public void Skill1_Attack()
        {   
            GameObject bullet = Instantiate(bulletObject, firePos.position, Quaternion.identity);
            
            if(!isFlip)
            {
                bullet.transform.localScale = new Vector3(-bullet.transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed;
            }
            else
            {
                
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
            }

            anim.SetTrigger("Skill1_Idle");
        }

        ///// <summary>
        ///// 박쥐를 소환해서 날리는 스킬을 구현한 함수
        ///// </summary>
        //private void Skill2()
        //{

        //}

        //private IEnumerator Co_Skill2()
        //{

        //}

        ///// <summary>
        ///// 불기둥을 만드는 함수
        ///// </summary>
        //private void Skill3()
        //{

        //}

        //private IEnumerator Co_Skill3()
        //{

        //}


    }
}
