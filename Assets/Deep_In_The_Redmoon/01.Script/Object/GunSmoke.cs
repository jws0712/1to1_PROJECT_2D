namespace OTO.Object
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// ���� �߻��Ҷ� ����� ������ ����� ������ Ŭ����
    /// </summary>
    public class GunSmoke : MonoBehaviour
    {
        /// <summary>
        /// ��ȯ�Ǹ� ���⸦ �ı���Ű�� �ڷ�ƾ ����
        /// </summary>
        private void OnEnable()
        {
            StartCoroutine(Co_DestroySmoke());
        }

        /// <summary>
        /// ���⸦ �ı��ϴ� �ڷ�ƾ
        /// </summary>
        /// <returns></returns>
        private IEnumerator Co_DestroySmoke()
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}


