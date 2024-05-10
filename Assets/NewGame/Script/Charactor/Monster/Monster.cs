namespace OTO.Charactor.Monster
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    public class Monster : Charactor
    {

        

        //Protected variables
        protected int MonsterBehavior;
        protected Rigidbody2D rb;

        //private variables

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Behavior���� return���ִ� �Լ�
        /// </summary>
        /// <returns></returns>
        protected virtual int MonsterMovement()
        {
            //������, ����, ���̵� ������ ����
            StartCoroutine(Co_SelectMovement());

            return MonsterBehavior;
            
        }

        // Ư���ð����� -1, 0, 1 �߿��� �Ѱ��� ���� Behavior������ �־��ִ� �ڷ�ƾ
        private IEnumerator Co_SelectMovement()
        {
            if (IsDead) yield break; //Dead ���¶�� �ڷ�ƾ�� ����
            MonsterBehavior = Random.Range(-1, 2); //-1, 0, 1 ������ ���� �������� MonsterBehavior�� �Ҵ���
            yield return new WaitForSeconds(1.2f); //1.2�� ���� �����
            StartCoroutine(Co_SelectMovement()); //�ٽ� �ڷ�ƾ�� ȣ���ؼ� ���ѹݺ���
        }

        protected virtual void FindPlayer()
        {
            //�÷��̾ �����ϴ� �ڵ带 ����
        }

        protected virtual void DropEx()
        {
            //����ġ�� ����ϴ� �ڵ带 ����
        }

        protected virtual void DropCoin()
        {
            //���� ����ϴ� �ڵ带 ����
        }
    }
}