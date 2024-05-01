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

    [Header("Script")]
    [SerializeField] private PlayerShot ShotScript;

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

        Direction = new Vector2(h, rb.velocity.y);
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
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(GroundCheckPos.position, 0.1f, GroundLayer);
    }

    private void Filp()
    {
        if(Mathf.Abs(ShotScript.RotZ) >= 100f && Isflip)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            ShotScript.HandPos.localScale = new Vector3(-1, -1, 1);
            Isflip = false;
        }
        if (Mathf.Abs(ShotScript.RotZ) <= 80f && !Isflip)
        {
            transform.localScale = new Vector3(1, 1, 1);
            ShotScript.HandPos.localScale = new Vector3(1, 1, 1);
            Isflip = true;
        }
    }
}
