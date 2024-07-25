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

       
        if(UserInterface._st>=10)
        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            anim.SetBool("isJump", true);
            UserInterface.UseStamina(30);
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

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어날 때 점프 불가능 상태로 변경
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았을 때 점프 가능 상태로 변경
        if (collision.gameObject.CompareTag("Ground"))
        {

            isGrounded = true;
            anim.SetBool("isJump", false);
        }

        if (collision.gameObject.tag == "trap")//trap에 닿았을떄 상호작용
        {
            OnDamaged(transform.position);
        }
        if (collision.gameObject.tag == "Enemy")//trap에 닿았을떄 상호작용
        {
            OnDamaged1(transform.position);
        }



    }
    private void OnDamaged(Vector2 targetPos)
    {
        rigid.velocity = Vector2.zero; // 기존 속도를 초기화
        gameObject.layer = 11; // layer 바꾸기
        

        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 히트 시 색변경& 투명화

        int dirc = rigid.velocityX > 0 ? 1 : -1;

        if(dirc == 1)
        {
            rigid.AddForce(new Vector2(8, 1) * 4, ForceMode2D.Impulse); // 히트 시 팅겨나가는 힘을 가함
            
         }
        else
        {
            rigid.AddForce(new Vector2(-8, 1) * 4, ForceMode2D.Impulse); // 히트 시 팅겨나가는 힘을 가함
        }


        Invoke("OffDamaged", 1); // 무적시간 해제
    }
    private void OnDamaged1(Vector2 targetPos)//적한테 히트시 
    {
        rigid.velocity = Vector2.zero; // 기존 속도를 초기화



        int dirc = rigid.velocityX > 0 ? 1 : -1;

        
        rigid.AddForce(new Vector2(dirc, 1) * 4, ForceMode2D.Impulse); // 히트 시 팅겨나가는 힘을 가함

        
    }

    private void Stamina(int Sta)
    {
        UserInterface.GetDamage(Sta);
    }


    void OffDamaged()
    {
        gameObject.layer = 10;//layer 바꾸기

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


}


