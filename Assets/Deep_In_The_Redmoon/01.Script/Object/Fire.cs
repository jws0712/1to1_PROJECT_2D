namespace OTO.Object
{
    using OTO.Charactor.Monster;
    using OTO.Charactor.Player;
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    public class Fire : MonoBehaviour
    {
        [Header("FireInfo")]
        [SerializeField] private float fireDamage = default;

        /// <summary>
        /// �ұ���� �ı��Ǵ� �Լ�
        /// </summary>
        private void DestroyObject()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// �ұ���� Ư�� �ױ׿� �ε������� ����Ǵ� �ڵ�
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerManager>().TakeDamage(fireDamage);
            }
        }
    }
}
