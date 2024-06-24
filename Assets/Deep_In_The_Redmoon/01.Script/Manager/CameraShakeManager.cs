namespace OTO.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// ī�޶��� ��鸲�� �����ϴ� Ŭ����
    /// </summary>
    public class CameraShakeManager : MonoBehaviour
    {
        public static CameraShakeManager instance;

        private Animator animator = null;

        private void Awake() //�̱���
        {
            instance = this;

            animator = GetComponent<Animator>(); //������Ʈ �ʱ�ȭ
        }

        /// <summary>
        /// ī�޶� ���� �ִϸ��̼��� �����ϴ� �Լ�
        /// </summary>
        /// <param name="name"></param>
        public void PlayShake(string name)
        {
            animator.SetTrigger(name);
        }
    }

}

