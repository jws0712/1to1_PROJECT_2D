namespace OTO.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using Unity.VisualScripting;

    //UnityEngine
    using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float moveSpeed;

        [Header("Jump")]
        [SerializeField] private float jumpPower;
        [SerializeField] private Transform groundCheckPos;
        [SerializeField] private LayerMask groundLayer;

        [Header("Dash")]
        [SerializeField] private float dashPower;
        [SerializeField] private float dashingTime;
        [SerializeField] private float dashingTimeCoolTime;


        [Header("Script")]
        [SerializeField] private PlayerShot shotScript;

        [Header("GhostEffect")]
        [SerializeField] private PlayerGhost playerGhost;
        
        //private variables
        private bool isJump = false;
        private bool isFilp = true;
        private Rigidbody2D rb;
        private Vector2 dir;
        private float horizontal;
        private bool isDash;
        private bool canDash = true;
        private GameObject GunObject;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GunObject = GameObject.FindGameObjectWithTag("Gun");
        }


        private void Update()
        {
            PlayerInput();
            PlayerJump();
            Filp();
            Dash();
        }

        private void FixedUpdate()
        {
            if (isDash)
            {
                return;
            }

            PlayerMove();
        }

        private void PlayerInput()
        {
            horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;

            dir = new Vector2(horizontal, rb.velocity.y);
        }
        private void PlayerMove()
        {
            rb.velocity = dir;
        }

        private void PlayerJump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                isJump = true;

                if(IsGround() == false)
                {
                    isJump = false;
                }
            }

            if (isJump == true && IsGround() && isDash == false)
            {
                rb.velocity = Vector2.zero;
                rb.velocity = Vector2.up * jumpPower;
                isJump = false;
            }
        }

        private bool IsGround()
        {
            return Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }

        private void Filp()
        {
            if(!isDash)
            {
                if (Mathf.Abs(shotScript.RotZ) >= 100f && isFilp)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    shotScript.HandPos.localScale = new Vector3(-1, -1, 1);
                    isFilp = false;
                }
                if (Mathf.Abs(shotScript.RotZ) <= 80f && !isFilp)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    shotScript.HandPos.localScale = new Vector3(1, 1, 1);
                    isFilp = true;
                }
            }
            else if (isDash)
            {
                if(horizontal < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    isFilp = false;
                    Debug.Log("¿ÞÂÊ");
                }
                else if(horizontal > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    isFilp = true;
                    Debug.Log("¿À¸¥ÂÊ");
                }
            }

        }

        private void Dash()
        {
            if(Input.GetMouseButtonDown(1) && canDash && horizontal != 0 && IsGround() == true)
            {
                StartCoroutine(Dashing());
            }
        }

        private IEnumerator Dashing()
        {
            rb.velocity = Vector2.zero;
            GunObject.SetActive(false);
            canDash = false;
            isDash = true;
            float orginGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.AddForce(Vector2.right * horizontal * dashPower, ForceMode2D.Impulse);
            playerGhost.makeGhost = true;
            yield return new WaitForSeconds(dashingTime);
            rb.gravityScale = orginGravity;
            isDash = false;
            yield return new WaitForSeconds(dashingTimeCoolTime);
            playerGhost.makeGhost = false;
            canDash = true;
            GunObject.SetActive(true);

        }
    }
}
