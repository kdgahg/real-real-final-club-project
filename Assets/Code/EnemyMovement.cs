using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    public int nextMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 5);
    }

    private void FixedUpdate()
    {
        // 이동
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 플랫폼 체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
            Turn();
    }

    // 행동을 결정하는 함수
    private void Think()
    {
        // 다음 행동 설정
        nextMove = Random.Range(-1, 2);

        // 스프라이트 애니메이션
        anim.SetInteger("WalkSpeed", nextMove);

        // 스프라이트 뒤집기
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        // 재귀 호출
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    // 방향을 전환하는 함수
    private void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }
}
