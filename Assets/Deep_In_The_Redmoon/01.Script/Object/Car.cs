namespace OTO.Object
{
    using OTO.Manager;
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// �ڵ����� ����� ������ Ŭ����
    /// </summary>
    public class Car : MonoBehaviour
    {
        [SerializeField] GameObject playerObject = null;
        [SerializeField] private float spawnPower = default;

        /// <summary>
        /// �÷��̾� ������Ʈ�� ��ȯ��Ű�� �Լ�
        /// </summary>
        public void SpawnPlayer()
        {
            AudioManager.instance.PlaySFX("PlayerJump");
            GameObject player = Instantiate(playerObject, transform.position, Quaternion.identity);

            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * spawnPower, ForceMode2D.Impulse);
        }
    }
}


