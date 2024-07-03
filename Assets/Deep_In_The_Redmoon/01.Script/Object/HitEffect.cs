namespace OTO.Object
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// �Ѿ��� �ı��ɶ� ��ȯ�Ǵ� ����Ʈ�� ����� ������ �Լ�
    /// </summary>
    public class HitEffect : MonoBehaviour
    {
        [SerializeField] private float destoryTime = default;

        /// <summary>
        /// ����Ʈ�� ��ȯ�Ǹ� �����ð� �ڿ� �ı��ǰ� �ϴ� �ڵ�
        /// </summary>
        private void Start()
        {
            Invoke("DestroyEffect", destoryTime);
        }

        /// <summary>
        /// ����Ʈ�� �ı��ϴ� �Լ�
        /// </summary>
        private void DestroyEffect()
        {
            Destroy(gameObject);
        }
    
    }
}


