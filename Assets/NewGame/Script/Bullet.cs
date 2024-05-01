using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private float BulletSpeed;
    [SerializeField] private float BulletDestroyTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestoryBullet", BulletDestroyTime);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.right * BulletSpeed * Time.deltaTime); ;
    }

    private void DestoryBullet()
    {
        Destroy(gameObject);
    }
}
