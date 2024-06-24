namespace OTO.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 카메라의 흔들림을 관리하는 클래스
    /// </summary>
    public class CameraShakeManager : MonoBehaviour
    {
        public static CameraShakeManager instance;

        private Animator animator = null;

        private void Awake() //싱글톤
        {
            instance = this;

            animator = GetComponent<Animator>(); //컴포넌트 초기화
        }

        /// <summary>
        /// 카메라를 흔드는 애니매이션을 실행하는 함수
        /// </summary>
        /// <param name="name"></param>
        public void PlayShake(string name)
        {
            animator.SetTrigger(name);
        }
    }

}

