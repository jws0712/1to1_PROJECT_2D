using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float Jump_speed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private float Horizontal;
    private GameObject Hand;


    private void Start()
    {
        Hand = GameObject.Find("Hand").gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            rb.AddForce(Vector2.up * Jump_speed, ForceMode2D.Impulse);
        }

        FlipX();
        MakeDir();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void MakeDir()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");


        
    }

    void PlayerMove()
    {
        rb.velocity = new Vector2(Horizontal * speed, rb.velocity.y);
    }

    void FlipX()
    {
        if (Mathf.Abs(SpinMananger1.Instance.rotZ) > Mathf.Abs(110))
        {
            transform.localScale = new Vector3(-2, 2, 2);
            Hand.transform.localScale = new Vector3(-1, -1, 1);
        }
        if (Mathf.Abs(SpinMananger1.Instance.rotZ) < Mathf.Abs(70))
        {
            transform.localScale = new Vector3(2, 2, 2);
            Hand.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
