using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    private bool isGrounded;
    public float jumpPower;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
       void Update()
    {
        if (isGrounded) 
        {
            if (Input.GetButtonDown("Jump"))
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        }
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        if (Mathf.Abs(rigid.velocity.x) > 0.3f)
        {
            anim.SetBool("isRun", true);
            Debug.Log("a");
        }
        else
        {
            anim.SetBool("isRun", false);
            Debug.Log("b");
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            isGrounded = true;
            Debug.Log("true");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            isGrounded = false;
            Debug.Log("false");
        }
    }
}
