namespace OTO.Object
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// ���� ���Լ� �������� �������� ����� ������ Ŭ����
    /// </summary>
    public class DropItem : MonoBehaviour
    {
        private Rigidbody2D rb = null;

        /// <summary>
        /// ������Ʈ �ʱ�ȭ
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// ���� ���������� �̲������� �ʰ� �ϴ� �ڵ�
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
