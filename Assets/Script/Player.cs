using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector2 dir;
    private GameObject Hand;

    private void Start()
    {
        Hand = GameObject.Find("Hand").gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void Update()
    {
        FlipX();
        MakeDir();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void MakeDir()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        dir = new Vector2(moveX, moveY).normalized;
    }

    void PlayerMove()
    {
        rb.velocity = dir * speed;
    }

    void FlipX()
    {
        if (Mathf.Abs(SpinMananger.Instance.rotZ) > Mathf.Abs(110))
        {
            transform.localScale = new Vector3(-2, 2, 2);
            Hand.transform.localScale = new Vector3(-1, -1, 1);
        }
        if (Mathf.Abs(SpinMananger.Instance.rotZ) < Mathf.Abs(70))
        {
            transform.localScale = new Vector3(2, 2, 2);
            Hand.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
