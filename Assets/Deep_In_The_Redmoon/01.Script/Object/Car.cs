namespace OTO.Object
{
    using OTO.Manager;
    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// 자동차의 기능을 구현한 클래스
    /// </summary>
    public class Car : MonoBehaviour
    {
        [SerializeField] GameObject playerObject = null;
        [SerializeField] private float spawnPower = default;

        /// <summary>
        /// 플레이어 오브젝트를 소환시키는 함수
        /// </summary>
        public void SpawnPlayer()
        {
            AudioManager.instance.PlaySFX("PlayerJump");
            GameObject player = Instantiate(playerObject, transform.position, Quaternion.identity);

            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * spawnPower, ForceMode2D.Impulse);
        }
    }
}


