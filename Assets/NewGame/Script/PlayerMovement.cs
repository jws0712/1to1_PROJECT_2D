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
        [SerializeField] private Transform GroundCheckPos;
        [SerializeField] private LayerMask GroundLayer;

        [Header("Dash")]
        [SerializeField] private float DashPower;
        [SerializeField] private float DashingTime;
        [SerializeField] private float DashingCoolTime;
        private bool IsDash;
        private bool CanDash = true;

        [Header("Script")]
        [SerializeField] private PlayerShot ShotScript;

        private bool Isflip = true;
        private Rigidbody2D rb;
        private Vector2 Direction;
        private float Horizontal;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
            if (IsDash)
            {
                return;
            }

            PlayerMove();
        }

        private void PlayerInput()
        {
            Horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;

            Direction = new Vector2(Horizontal, rb.velocity.y);
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
            if (Mathf.Abs(ShotScript.RotZ) >= 100f && Isflip)
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

        private void Dash()
        {
            if(Input.GetMouseButtonDown(1) && CanDash && Horizontal != 0)
            {
                Debug.Log("´ë½Ã");
                StartCoroutine(Dashing());

            }
        }

        private IEnumerator Dashing()
        {
            CanDash = false;
            IsDash = true;
            float orginGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.AddForce(Vector2.right * Horizontal * DashPower, ForceMode2D.Impulse);
            yield return new WaitForSeconds(DashingTime);
            rb.gravityScale = orginGravity;
            IsDash = false;
            yield return new WaitForSeconds(DashingCoolTime);
            CanDash = true;
        }
    }
}
