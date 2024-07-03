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
                        Debug.Log("����ġ ���̵�");
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
        /// �⺻ �����϶� ����Ǵ� �Լ�
        /// </summary>
        private void Idle()
        {
            Debug.Log("���̵�");
            transform.position = Vector2.up * 5;
            StartCoroutine(Co_Idle());
        }

        private IEnumerator Co_Idle()
        {
            anim.SetTrigger("Idle");
            yield return new WaitForSeconds(3);
            Debug.Log("�ٲ�");
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
        /// �˱⸦ ������ ��ų�� ������ �Լ�
        /// </summary>
        private void Skill1()
        {
            Debug.Log("��ų1");
            batBossState = BatBossState.Idle;
            anim.SetTrigger("Smoke");
        }

        //private IEnumerator Co_Skill1()
        //{

        //}

        ///// <summary>
        ///// ���㸦 ��ȯ�ؼ� ������ ��ų�� ������ �Լ�
        ///// </summary>
        //private void Skill2()
        //{

        //}

        //private IEnumerator Co_Skill2()
        //{

        //}

        ///// <summary>
        ///// �ұ���� ����� �Լ�
        ///// </summary>
        //private void Skill3()
        //{

        //}

        //private IEnumerator Co_Skill3()
        //{

        //}

    }
}
