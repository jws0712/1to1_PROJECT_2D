namespace OTO.Charactor.Player
{
    using OTO.Manager;
    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;

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
        [SerializeField] private float oringGravitySacle = default;
        [SerializeField] private float fallGravitySacle = default;

        [Header("Dash")]
        [SerializeField] private float dashPower;
        [SerializeField] private float dashingTime;
        [SerializeField] private float dashingTimeCoolTime;


        [Header("Script")]
        [SerializeField] private PlayerShot shotScript;


        [Header("GhostEffect")]
        [SerializeField] private PlayerGhost playerGhost;
        
        //private variables
        private Vector2 dir;
        private LayerMask monsterLayer = default;
        private LayerMask playerLayer = default;
        private GameObject handPos = default;
        private GameObject GunObject;
        private Animator animator = null;
        private Rigidbody2D rb;
        private bool canDash = true;
        private float horizontal = default;

        //public variables
        public bool isFilp = true;
        public bool isDash;

        /// <summary>
        /// ������Ʈ �ʱ�ȭ
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// ���� �ʱ�ȭ
        /// </summary>
        private void Start()
        {
            monsterLayer = LayerMask.NameToLayer("Monster");
            playerLayer = LayerMask.NameToLayer("Player");
        }

        /// <summary>
        /// �÷��̾��� �Է°� �ൿ���� ������
        /// </summary>
        private void Update()
        {
            if(GameManager.instance.isGameOver == true)
            {
                return;
            }

            PlayerInput();
            PlayerJump();
            Filp();
            Dash();
            PlayerAnimation();

            handPos = GameObject.FindGameObjectWithTag("HandPos");

            GunObject = handPos.transform.GetChild(0).gameObject;
        }

        /// <summary>
        /// �÷��̾��� �������� ������
        /// </summary>
        private void FixedUpdate()
        {
            if (isDash)
            {
                return;
            }

            PlayerMove();
        }

        /// <summary>
        /// �÷��̾��� �Է��� �����ϴ� �Լ�
        /// </summary>
        private void PlayerInput()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        /// <summary>
        /// �÷��̾��� �������� �����ϴ� �Լ�
        /// </summary>
        private void PlayerMove()
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }

        /// <summary>
        /// �÷��̾��� ���� ����� ������ �Լ�
        /// </summary>
        private void PlayerJump()
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallGravitySacle;
            }
            else
            {
                rb.gravityScale = oringGravitySacle;
            }

            if (Input.GetButtonDown("Jump") && CheckGround() == true && !isDash)
            {

                AudioManager.instance.PlaySFX("PlayerJump");
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            }
            
            if(CheckGround() == true)
            {
                animator.SetBool("isJump", false);
            }
            else
            {
                animator.SetBool("isJump", true);
            }

        }

        /// <summary>
        /// �÷��̾� ������Ʈ�� ������ �Լ�
        /// </summary>
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

        /// <summary>
        /// ��ø� �����ϴ� �Լ�
        /// </summary>
        private void Dash()
        {
            if(Input.GetMouseButtonDown(1) && canDash && horizontal != 0 && CheckGround() == true)
            {
                StartCoroutine(Dashing());
            }
        }

        /// <summary>
        /// ����� ����� ������ �Լ�
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// �÷��̾��� �ִϸ��̼� ���¸� �����ϴ� �Լ�
        /// </summary>
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

        /// <summary>
        /// ���� ��Ҵ��� �˻��ϴ� �Լ�
        /// </summary>
        /// <returns></returns>
        private bool CheckGround()
        {
            return Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }
}
