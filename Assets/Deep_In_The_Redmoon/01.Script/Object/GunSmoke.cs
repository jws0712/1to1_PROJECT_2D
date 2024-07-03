namespace OTO.Object
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// 총을 발사할때 생기는 연기의 기능을 구현한 클래스
    /// </summary>
    public class GunSmoke : MonoBehaviour
    {
        /// <summary>
        /// 소환되면 연기를 파괴시키는 코루틴 실행
        /// </summary>
        private void OnEnable()
        {
            StartCoroutine(Co_DestroySmoke());
        }

        /// <summary>
        /// 연기를 파괴하는 코루틴
        /// </summary>
        /// <returns></returns>
        private IEnumerator Co_DestroySmoke()
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}


