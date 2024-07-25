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
    private bool isDamaged = false; // 피해 여부를 추적하기 위한 변수

    public UserInterface UserInterface;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
<<<<<<< Updated upstream
        // �̵� �Է� ó��
        moveInput = Input.GetAxis("Horizontal");

       
        if(UserInterface._st>=10)
        // ���� ó��
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
=======
        if (isDamaged) return; // 피해 상태일 때는 입력을 무시

        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && UserInterface._st >= 10)
>>>>>>> Stashed changes
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            anim.SetBool("isJump", true);
            UserInterface.UseStamina(30);
        }
<<<<<<< Updated upstream

        // �ִϸ��̼� ó��
        if (Mathf.Abs(moveInput) > 0.3f)
=======

        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        // 이동 방향에 따른 스프라이트 반전
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }

        // 애니메이션 처리
        if (Mathf.Abs(rigid.velocity.x) > 0.3f)
>>>>>>> Stashed changes
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
<<<<<<< Updated upstream

        // �̵� ���⿡ ���� ��������Ʈ ����
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
=======
>>>>>>> Stashed changes
    }

    private void FixedUpdate()
    {
<<<<<<< Updated upstream
        // �̵� ó��
        rigid.velocity = new Vector2(moveInput * speed, rigid.velocity.y);
=======
        if (isDamaged) return; // 피해 상태일 때는 입력을 무시

        float h = moveInput; // moveInput을 사용하여 방향 전환

        rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) // 최대 속도 제한
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed) // 반대 방향 최대 속도 제한
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
>>>>>>> Stashed changes
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
<<<<<<< Updated upstream
        // �ٴڿ��� ��� �� ���� �Ұ��� ���·� ����
=======
        // 바닥에서 떨어졌을 때 착지 상태 해제
>>>>>>> Stashed changes
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< Updated upstream
        // �ٴڿ� ����� �� ���� ���� ���·� ����
=======
        // 바닥에 착지했을 때 착지 상태 설정
>>>>>>> Stashed changes
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }

<<<<<<< Updated upstream
        if (collision.gameObject.tag == "trap")//trap�� ������� ��ȣ�ۿ�
        {
            OnDamaged(transform.position);
        }
        if (collision.gameObject.tag == "Enemy")//trap�� ������� ��ȣ�ۿ�
=======
        // 트랩에 부딪혔을 때
        if (collision.gameObject.tag == "trap")
        {
            OnDamaged(transform.position);
        }

        // 적에 부딪혔을 때
        if (collision.gameObject.tag == "Enemy")
>>>>>>> Stashed changes
        {
            OnDamaged1(transform.position);
        }
    }

    private void OnDamaged(Vector2 targetPos)
    {
<<<<<<< Updated upstream
        rigid.velocity = Vector2.zero; // ���� �ӵ��� �ʱ�ȭ
        gameObject.layer = 11; // layer �ٲٱ�
        

        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // ��Ʈ �� ������& ����ȭ
=======
        UserInterface.UseHp(Trap_damage);
        rigid.velocity = Vector2.zero; // 이동 속도 초기화
        gameObject.layer = 11; // layer 변경
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 스프라이트 반투명 처리

        isDamaged = true; // 피해 상태 설정
>>>>>>> Stashed changes

        int dirc = rigid.velocity.x > 0 ? 1 : -1;

        if (dirc == 1)
        {
<<<<<<< Updated upstream
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
=======
            rigid.AddForce(new Vector2(0.5f, 1) * 4, ForceMode2D.Impulse); // 반대 방향으로 밀어내기
        }
        else
        {
            rigid.AddForce(new Vector2(-0.5f, 1) * 4, ForceMode2D.Impulse); // 반대 방향으로 밀어내기
        }

        Invoke("OffDamaged", 1); // 일정 시간 후 피해 상태 해제
    }

    private void OnDamaged1(Vector2 targetPos)
    {
        rigid.velocity = Vector2.zero; // 이동 속도 초기화
>>>>>>> Stashed changes

        isDamaged = true; // 피해 상태 설정

<<<<<<< Updated upstream

        int dirc = rigid.velocityX > 0 ? 1 : -1;

        
        rigid.AddForce(new Vector2(dirc, 1) * 4, ForceMode2D.Impulse); // ��Ʈ �� �ðܳ����� ���� ����

        
=======
        int dirc = rigid.velocity.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 4, ForceMode2D.Impulse); // 반대 방향으로 밀어내기
>>>>>>> Stashed changes
    }

    private void Stamina(int Sta)
    {
        UserInterface.GetDamage(Sta);
    }

    void OffDamaged()
    {
<<<<<<< Updated upstream
        gameObject.layer = 10;//layer �ٲٱ�

        spriteRenderer.color = new Color(1, 1, 1, 1);
=======
        isDamaged = false; // 피해 상태 해제
        gameObject.layer = 10; // layer 변경
        spriteRenderer.color = new Color(1, 1, 1, 1); // 스프라이트 원래대로 돌리기
>>>>>>> Stashed changes
    }
}
