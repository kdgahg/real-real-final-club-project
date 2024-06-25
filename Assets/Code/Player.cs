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
    public float maxSpeed;
    public int Trap_damage;

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

       

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && UserInterface._st>=10)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            UserInterface.UseStamina(30);
        }
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f,rigid.velocity.y);
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;


        // �ִϸ��̼� ó��
        if (Mathf.Abs(rigid.velocity.x) > 0.3f)
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
         float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)//������ �ִ� �ӵ�
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1))//���� �ִ�ӵ�
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
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
        UserInterface.UseHp(Trap_damage);
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


