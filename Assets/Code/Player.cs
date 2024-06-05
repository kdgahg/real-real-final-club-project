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
        // �̵� �Է� ó��
        moveInput = Input.GetAxis("Horizontal");

        // ���� ó��
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            anim.SetBool("isJump", true);
        }

        // �ִϸ��̼� ó��
        if (Mathf.Abs(moveInput) > 0.3f)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        // �̵� ���⿡ ���� ��������Ʈ ����
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
    }

    private void FixedUpdate()
    {
        // �̵� ó��
        rigid.velocity = new Vector2(moveInput * speed, rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ����� �� ���� ���� ���·� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            isGrounded = true;
            anim.SetBool("isJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ��� ��� �� ���� �Ұ��� ���·� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}


