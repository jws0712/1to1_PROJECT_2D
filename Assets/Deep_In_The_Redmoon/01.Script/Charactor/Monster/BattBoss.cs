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
            chaseHouse = false; //���� �߰����� ����
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
        /// �⺻ �����϶� ����Ǵ� �Լ�
        /// </summary>
        private void Idle()
        {

        }

        /// <summary>
        /// �˱⸦ ������ ��ų�� ������ �Լ�
        /// </summary>
        private void Skill1()
        {

        }

        /// <summary>
        /// ���㸦 ��ȯ�ؼ� ������ ��ų�� ������ �Լ�
        /// </summary>
        private void Skill2()
        {

        }

        /// <summary>
        /// �ұ���� ����� �Լ�
        /// </summary>
        private void Skill3()
        {

        }

    }
}
