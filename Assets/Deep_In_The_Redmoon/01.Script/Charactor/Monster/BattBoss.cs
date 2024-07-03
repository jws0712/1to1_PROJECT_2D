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

        private Animator animator = null;

        protected override void OnEnable()
        {
            chaseHouse = false; //집을 추격하지 않음
            base.OnEnable();
            animator = GetComponent<Animator>();
            currentCoolTime = 0f;

            animator.SetTrigger("Smoke");
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
                        Idle();
                        break;
                    }
                case BatBossState.Skill1:
                    {
                        Skill1();
                        break;
                    }
                case BatBossState.Skill2:
                    {
                        Skill2();
                        break;
                    }
                case BatBossState.Skill3:
                    {
                        Skill3();
                        break;
                    }
            }
        }

        /// <summary>
        /// 기본 상태일때 실행되는 함수
        /// </summary>
        private void Idle()
        {

        }

        /// <summary>
        /// 검기를 날리는 스킬을 구현한 함수
        /// </summary>
        private void Skill1()
        {

        }

        /// <summary>
        /// 박쥐를 소환해서 날리는 스킬을 구현한 함수
        /// </summary>
        private void Skill2()
        {

        }

        /// <summary>
        /// 불기둥을 만드는 함수
        /// </summary>
        private void Skill3()
        {

        }

    }
}
