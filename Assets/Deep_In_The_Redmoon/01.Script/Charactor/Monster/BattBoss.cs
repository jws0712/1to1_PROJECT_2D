namespace OTO.Charactor.Monster
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class BattBoss : Monster
    {   
        private enum BatBossState
        {
            Idle,
            Skill1,
            Skill2,
            Skill3,
        }

        private BatBossState batBossState;

        protected override void OnEnable()
        {
            base.OnEnable();
            currentCoolTime = 0f;
            anim.SetTrigger("Smoke");
            batBossState = BatBossState.Idle;
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Attack()
        {
            base.Attack();

            //if(isAttack == true)
            //{

            //}
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
            Debug.Log("아이들");
            transform.position = Vector2.up * 5;
            StartCoroutine(Co_Idle());
        }

        private IEnumerator Co_Idle()
        {
            anim.SetTrigger("Idle");
            yield return new WaitForSeconds(3);
            Debug.Log("바꿔");
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
            Debug.Log("스킬1");
            batBossState = BatBossState.Idle;
            anim.SetTrigger("Smoke");
        }

        //private IEnumerator Co_Skill1()
        //{

        //}

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
