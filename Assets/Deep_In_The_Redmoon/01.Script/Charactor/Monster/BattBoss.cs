namespace OTO.Charactor.Monster
{
    using OTO.Object;

    //System
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;

    //UnityEngine
    using UnityEngine;

    public class BattBoss : Monster
    {   
        /// <summary>
        /// ������ ����
        /// </summary>
        private enum BatBossState
        {
            Idle,
            Skill1,
            Skill2,
            Skill3,
        }

        private BatBossState batBossState;

        [Header("Skill")]
        [SerializeField]
        private GameObject bulletObject = null;
        [SerializeField]
        private GameObject fireObject = null;
        [SerializeField]
        private Transform shotPos = null;

        //private variables
        private Transform playerPos = default;

        /// <summary>
        /// ���� �ʱ�ȭ
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            currentCoolTime = 0f;

            batBossState = BatBossState.Idle;
            anim.SetTrigger("Smoke");
            
        }

        /// <summary>
        /// �÷��̾��� ��ġ�� ���� �ø��ϴ� �ڵ�
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

        /// <summary>
        /// ������ �ൿ ���¸� �����ϴ� �Լ�
        /// </summary>
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
                case BatBossState.Skill2:
                    {
                        Skill2();
                        break;
                    }
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
            transform.position = new Vector3(0, 0.3463205f, 0);
            StartCoroutine(Co_Idle());
        }

        /// <summary>
        /// �⺻ �����϶� ����Ǵ� �ڷ�ƾ
        /// </summary>
        /// <returns></returns>
        private IEnumerator Co_Idle()
        {
            anim.SetTrigger("Idle");
            yield return new WaitForSeconds(3);
            int skillIndex = Random.Range(1, 3);

            switch (skillIndex)
            {
                case 1:
                    {
                        batBossState = BatBossState.Skill1;
                        break;
                    }
                case 2:
                    {
                        batBossState = BatBossState.Skill2;
                        break;
                    }
            }

            anim.SetTrigger("Smoke");


        }

        /// <summary>
        /// �˱⸦ ������ ��ų�� �����ϴ� �Լ�
        /// </summary>
        private void Skill1()
        {
            StartCoroutine(Co_Skill1());
        }

        /// <summary>
        /// �˱⸦ ������ ������ ������ �ڷ�ƾ
        /// </summary>
        /// <returns></returns>
        private IEnumerator Co_Skill1()
        {
            transform.position = new Vector2(playerPos.position.x + 15, 0.3463205f);

            anim.SetTrigger("Skill1");

            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1.5f);
                anim.SetTrigger("Skill1_Attack");
            }

            batBossState = BatBossState.Idle;
            anim.SetTrigger("Smoke");

        }

        /// <summary>
        /// �˱⸦ ������ �Լ�
        /// </summary>
        public void Skill1_Attack()
        {   
            GameObject bullet = Instantiate(bulletObject, shotPos.position, Quaternion.identity);
            
            if(isFlip)
            {
                bullet.GetComponent<Bullet>().BulletSpeed *= -1;
            }
            else
            {
                bullet.transform.localScale = new Vector3(-bullet.transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
            }

            anim.SetTrigger("Skill1_Idle");
        }

        /// <summary>
        /// �ұ���� ��ų�� �����ϴ� �Լ�
        /// </summary>
        private void Skill2()
        {
            anim.SetTrigger("Skill2");

            transform.position = new Vector2(0, 10.35f);
            StartCoroutine(Co_Skill2());
        }

        /// <summary>
        /// �ұ���� ��ȯ ��Ű�� �Լ�
        /// </summary>
        /// <returns></returns>
        private IEnumerator Co_Skill2()
        {
            yield return new WaitForSeconds(2);
            int fireCount = Random.Range(1, 4);

            for(int i = 0; i < fireCount; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Instantiate(fireObject, new Vector3(50 - 10 * j, 2f, playerPos.position.z), Quaternion.identity);
                }
                yield return new WaitForSeconds(0.5f);
                for (int k = 0; k < 10; k++)
                {
                    Instantiate(fireObject, new Vector3(45 - 10 * k, 2f, playerPos.position.z), Quaternion.identity);
                }
                yield return new WaitForSeconds(0.5f);
            }

            batBossState = BatBossState.Idle;
            anim.SetTrigger("Smoke");
        }
    }
}
