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

       
        if(UserInterface._st>=10)
        // ���� ó��
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            anim.SetBool("isJump", true);
            UserInterface.UseStamina(30);
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

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ��� ��� �� ���� �Ұ��� ���·� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ����� �� ���� ���� ���·� ����
        if (collision.gameObject.CompareTag("Ground"))
        {

            isGrounded = true;
            anim.SetBool("isJump", false);
        }

        if (collision.gameObject.tag == "trap")//trap�� ������� ��ȣ�ۿ�
        {
            OnDamaged(transform.position);
        }
        if (collision.gameObject.tag == "Enemy")//trap�� ������� ��ȣ�ۿ�
        {
            OnDamaged1(transform.position);
        }



    }
    private void OnDamaged(Vector2 targetPos)
    {
        rigid.velocity = Vector2.zero; // ���� �ӵ��� �ʱ�ȭ
        gameObject.layer = 11; // layer �ٲٱ�
        

        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // ��Ʈ �� ������& ����ȭ

        int dirc = rigid.velocityX > 0 ? 1 : -1;

        if(dirc == 1)
        {
            rigid.AddForce(new Vector2(8, 1) * 4, ForceMode2D.Impulse); // ��Ʈ �� �ðܳ����� ���� ����
            
         }
        else
        {
            rigid.AddForce(new Vector2(-8, 1) * 4, ForceMode2D.Impulse); // ��Ʈ �� �ðܳ����� ���� ����
        }


        Invoke("OffDamaged", 1); // �����ð� ����
    }
    private void OnDamaged1(Vector2 targetPos)//������ ��Ʈ�� 
    {
        rigid.velocity = Vector2.zero; // ���� �ӵ��� �ʱ�ȭ



        int dirc = rigid.velocityX > 0 ? 1 : -1;

        
        rigid.AddForce(new Vector2(dirc, 1) * 4, ForceMode2D.Impulse); // ��Ʈ �� �ðܳ����� ���� ����

        
    }

    private void Stamina(int Sta)
    {
        UserInterface.GetDamage(Sta);
    }


    void OffDamaged()
    {
        gameObject.layer = 10;//layer �ٲٱ�

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


}


