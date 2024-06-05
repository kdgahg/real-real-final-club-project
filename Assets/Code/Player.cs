using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private bool isGrounded;

    private float moveInput;
    
    public UserInterface UserInterface;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        // 이동 입력 처리
        moveInput = Input.GetAxis("Horizontal");

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            anim.SetBool("isJump", true);
        }

        // 애니메이션 처리
        if (Mathf.Abs(moveInput) > 0.3f)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        // 이동 방향에 따라 스프라이트 반전
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
    }

    private void FixedUpdate()
    {
        // 이동 처리
        rigid.velocity = new Vector2(moveInput * speed, rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았을 때 점프 가능 상태로 변경
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            isGrounded = true;
            anim.SetBool("isJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어날 때 점프 불가능 상태로 변경
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}


