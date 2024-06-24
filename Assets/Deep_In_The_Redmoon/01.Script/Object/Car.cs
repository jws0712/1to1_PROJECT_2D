using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] GameObject playerObject = null;
    [SerializeField] private float spawnPower = default;
    public void SpawnPlayer()
    {
        GameObject player = Instantiate(playerObject, transform.position, Quaternion.identity);

        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * spawnPower, ForceMode2D.Impulse);
    }
}
