using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMove")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;

    [Header("Jump")]
    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private LayerMask GroundLayer;

    private Rigidbody2D rb; 
    private Vector2 Direction; 


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        PlayerInput();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        float h = Input.GetAxisRaw("Horizontal") * moveSpeed;

        Direction = new Vector2(h, rb.velocity.y);
    }
    private void PlayerMove()
    {
        rb.velocity = Direction;
    }

    private void PlayerJump()
    {
        if(Input.GetButton("Jump") && IsGround())
        {
            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.up * jumpPower;
        }

        Debug.Log(IsGround());
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(GroundCheckPos.position, 0.1f, GroundLayer);
    }
}
