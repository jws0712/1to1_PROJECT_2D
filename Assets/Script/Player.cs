using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector2 dir;
    private Vector3 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void Update()
    {
        mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(rotZ) > Mathf.Abs(110))
        {
            Debug.Log("����!");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Mathf.Abs(rotZ) < Mathf.Abs(70))
        {
            Debug.Log("������ �־�!");

            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        dir = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }
}
