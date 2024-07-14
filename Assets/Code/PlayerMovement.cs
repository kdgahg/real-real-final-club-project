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
        if (isDamaged) return; // 피해 상태일 때는 입력을 무시

        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && UserInterface._st >= 10)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            UserInterface.UseStamina(30);
        }

        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        // 이동 방향에 따른 스프라이트 반전
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }

        // 애니메이션 처리
        if (Mathf.Abs(rigid.velocity.x) > 0.3f)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    private void FixedUpdate()
    {
        if (isDamaged) return; // 피해 상태일 때는 입력을 무시

        float h = moveInput; // moveInput을 사용하여 방향 전환

        rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) // 최대 속도 제한
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed) // 반대 방향 최대 속도 제한
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 떨어졌을 때 착지 상태 해제
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 착지했을 때 착지 상태 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }

        // 트랩에 부딪혔을 때
        if (collision.gameObject.tag == "trap")
        {
            OnDamaged(collision.transform.position);
        }

        // 적에 부딪혔을 때
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged1(collision.transform.position);
        }

        // 슬라임에 부딪혔을 때
        if (collision.gameObject.tag == "Slime")
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnDamaged1(collision.transform.position);
                
            }
            else
            {
                OnDamaged2(collision.transform.position);
                UserInterface.UseHp(50);
            }
        }
    }

        private void OnDamaged(Vector2 targetPos)
        {
            UserInterface.UseHp(Trap_damage);
            rigid.velocity = Vector2.zero; // 이동 속도 초기화
            gameObject.layer = 11; // layer 변경
            spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 스프라이트 반투명 처리

            isDamaged = true; // 피해 상태 설정

            // Calculate direction based on the target position
            Vector2 hitDirection = (transform.position - (Vector3)targetPos).normalized;
            rigid.AddForce(hitDirection * 4, ForceMode2D.Impulse); // 반대 방향으로 밀어내기

            Invoke("OffDamaged", 1); // 일정 시간 후 피해 상태 해제
        }

        private void OnDamaged1(Vector2 targetPos)
        {
            rigid.velocity = Vector2.zero; // 이동 속도 초기화

            isDamaged = true; // 피해 상태 설정

            // Calculate direction based on the target position
            Vector2 hitDirection = (transform.position - (Vector3)targetPos).normalized;
            rigid.AddForce(hitDirection * 4, ForceMode2D.Impulse); // 반대 방향으로 밀어내기

            Invoke("OffDamaged", 1); // 일정 시간 후 피해 상태 해제
        }
        private void OnDamaged2(Vector2 targetPos)
        {
            UserInterface.UseHp(Trap_damage);
            rigid.velocity = Vector2.zero; // 이동 속도 초기화
            gameObject.layer = 11; // layer 변경
            spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 스프라이트 반투명 처리

            isDamaged = true; // 피해 상태 설정

            // Calculate direction based on the target position
            Vector2 hitDirection = (transform.position - (Vector3)targetPos).normalized;
            rigid.AddForce(hitDirection * 8, ForceMode2D.Impulse); // 반대 방향으로 밀어내기

            Invoke("OffDamaged", 1); // 일정 시간 후 피해 상태 해제
        }
        void OffDamaged()
        {
            isDamaged = false; // 피해 상태 해제
            gameObject.layer = 10; // layer 변경
            spriteRenderer.color = new Color(1, 1, 1, 1); // 스프라이트 원래대로 돌리기
        }
    }
