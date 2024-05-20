namespace OTO.Charactor.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    //Unity
    using Unity.VisualScripting;
    using UnityEditor.U2D.Aseprite;

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
        [SerializeField] private PlayerHp playerHp;


        [Header("GhostEffect")]
        [SerializeField] private PlayerGhost playerGhost;
        
        //private variables
        private bool isJump = false;
        private Vector2 dir;
        private bool canDash = true;
        private GameObject GunObject;
        private LayerMask monsterLayer = default;
        private LayerMask playerLayer = default;
        private Animator animator = null;
        private bool isGround = default;
        private bool isHit = default;

        //public variables
        public Rigidbody2D rb;
        public float horizontal;
        public bool isDash;
        public bool isFilp = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            GunObject = GameObject.FindGameObjectWithTag("Gun");

            monsterLayer = LayerMask.NameToLayer("Monster");
            playerLayer = LayerMask.NameToLayer("Player");
        }


        private void Update()
        {
            PlayerInput();
            PlayerJump();
            Filp();
            Dash();
            PlayerAnimation();
            IsGround();
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
            return isGround;
        }

        private void Filp()
        {
            if (!isDash)
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
            else
            {
                if (horizontal < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    isFilp = false;
                }
                else if (horizontal > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    isFilp = true;
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

            Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, true);

            yield return new WaitForSeconds(dashingTime);

            rb.gravityScale = orginGravity;
            isDash = false;

            yield return new WaitForSeconds(dashingTimeCoolTime);

            Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, false);

            playerGhost.makeGhost = false;
            canDash = true;

            GunObject.SetActive(true);

        }

        private void PlayerAnimation()
        {
            if(horizontal != 0)
            {
                animator.SetBool("isWalk", true);
            }
            else
            {
                animator.SetBool("isWalk", false);
            }

            animator.SetBool("isDash", isDash);
        }
        public void KnockBack()
        {   
            if(isHit == true)
            {
                rb.AddForce(Vector2.right * 1000f , ForceMode2D.Impulse);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Ground"))
            {
                isGround = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isGround = false;
        }
    }
}
