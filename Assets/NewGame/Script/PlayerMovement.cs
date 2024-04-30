using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMove")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;

    [Header("Jump")]
    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private LayerMask GroundLayer;

    private bool Isflip = true;
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
        Filp();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        float h = Input.GetAxisRaw("Horizontal") * moveSpeed;
//        float v = Input.GetAxisRaw("Vertical") * moveSpeed;

        Direction = new Vector2(h, rb.velocity.y);
//        Direction = new Vector2(h, v);
    }
    private void PlayerMove()
    {
        rb.velocity = Direction;
    }

    private void PlayerJump()
    {
        if (Input.GetButton("Jump") && IsGround())
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

    private void Filp()
    {
        if(Mathf.Abs(PlayerShot.instance.RotZ) >= 100f && Isflip)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Isflip = false;
        }
        if (Mathf.Abs(PlayerShot.instance.RotZ) <= 80f && !Isflip)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Isflip = true;
        }
    }
}
