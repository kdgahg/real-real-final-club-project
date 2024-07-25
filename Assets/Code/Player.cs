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
    private bool isDamaged = false; // í”¼í•´ ì—¬ë¶€ë¥¼ ì¶”ì í•˜ê¸° ìœ„í•œ ë³€ìˆ˜

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
        // ÀÌµ¿ ÀÔ·Â Ã³¸®
        moveInput = Input.GetAxis("Horizontal");

       
        if(UserInterface._st>=10)
        // Á¡ÇÁ Ã³¸®
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
=======
        if (isDamaged) return; // í”¼í•´ ìƒíƒœì¼ ë•ŒëŠ” ì…ë ¥ì„ ë¬´ì‹œ

        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && UserInterface._st >= 10)
>>>>>>> Stashed changes
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            anim.SetBool("isJump", true);
            UserInterface.UseStamina(30);
        }
<<<<<<< Updated upstream

        // ¾Ö´Ï¸ŞÀÌ¼Ç Ã³¸®
        if (Mathf.Abs(moveInput) > 0.3f)
=======

        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        // ì´ë™ ë°©í–¥ì— ë”°ë¥¸ ìŠ¤í”„ë¼ì´íŠ¸ ë°˜ì „
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }

        // ì• ë‹ˆë©”ì´ì…˜ ì²˜ë¦¬
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

        // ÀÌµ¿ ¹æÇâ¿¡ µû¶ó ½ºÇÁ¶óÀÌÆ® ¹İÀü
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
        // ÀÌµ¿ Ã³¸®
        rigid.velocity = new Vector2(moveInput * speed, rigid.velocity.y);
=======
        if (isDamaged) return; // í”¼í•´ ìƒíƒœì¼ ë•ŒëŠ” ì…ë ¥ì„ ë¬´ì‹œ

        float h = moveInput; // moveInputì„ ì‚¬ìš©í•˜ì—¬ ë°©í–¥ ì „í™˜

        rigid.AddForce(Vector2.right * h * speed, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) // ìµœëŒ€ ì†ë„ ì œí•œ
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed) // ë°˜ëŒ€ ë°©í–¥ ìµœëŒ€ ì†ë„ ì œí•œ
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
>>>>>>> Stashed changes
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
<<<<<<< Updated upstream
        // ¹Ù´Ú¿¡¼­ ¹ş¾î³¯ ¶§ Á¡ÇÁ ºÒ°¡´É »óÅÂ·Î º¯°æ
=======
        // ë°”ë‹¥ì—ì„œ ë–¨ì–´ì¡Œì„ ë•Œ ì°©ì§€ ìƒíƒœ í•´ì œ
>>>>>>> Stashed changes
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< Updated upstream
        // ¹Ù´Ú¿¡ ´ê¾ÒÀ» ¶§ Á¡ÇÁ °¡´É »óÅÂ·Î º¯°æ
=======
        // ë°”ë‹¥ì— ì°©ì§€í–ˆì„ ë•Œ ì°©ì§€ ìƒíƒœ ì„¤ì •
>>>>>>> Stashed changes
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }

<<<<<<< Updated upstream
        if (collision.gameObject.tag == "trap")//trap¿¡ ´ê¾ÒÀ»‹š »óÈ£ÀÛ¿ë
        {
            OnDamaged(transform.position);
        }
        if (collision.gameObject.tag == "Enemy")//trap¿¡ ´ê¾ÒÀ»‹š »óÈ£ÀÛ¿ë
=======
        // íŠ¸ë©ì— ë¶€ë”ªí˜”ì„ ë•Œ
        if (collision.gameObject.tag == "trap")
        {
            OnDamaged(transform.position);
        }

        // ì ì— ë¶€ë”ªí˜”ì„ ë•Œ
        if (collision.gameObject.tag == "Enemy")
>>>>>>> Stashed changes
        {
            OnDamaged1(transform.position);
        }
    }

    private void OnDamaged(Vector2 targetPos)
    {
<<<<<<< Updated upstream
        rigid.velocity = Vector2.zero; // ±âÁ¸ ¼Óµµ¸¦ ÃÊ±âÈ­
        gameObject.layer = 11; // layer ¹Ù²Ù±â
        

        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // È÷Æ® ½Ã »öº¯°æ& Åõ¸íÈ­
=======
        UserInterface.UseHp(Trap_damage);
        rigid.velocity = Vector2.zero; // ì´ë™ ì†ë„ ì´ˆê¸°í™”
        gameObject.layer = 11; // layer ë³€ê²½
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // ìŠ¤í”„ë¼ì´íŠ¸ ë°˜íˆ¬ëª… ì²˜ë¦¬

        isDamaged = true; // í”¼í•´ ìƒíƒœ ì„¤ì •
>>>>>>> Stashed changes

        int dirc = rigid.velocity.x > 0 ? 1 : -1;

        if (dirc == 1)
        {
<<<<<<< Updated upstream
            rigid.AddForce(new Vector2(8, 1) * 4, ForceMode2D.Impulse); // È÷Æ® ½Ã ÆÃ°Ü³ª°¡´Â ÈûÀ» °¡ÇÔ
            
         }
        else
        {
            rigid.AddForce(new Vector2(-8, 1) * 4, ForceMode2D.Impulse); // È÷Æ® ½Ã ÆÃ°Ü³ª°¡´Â ÈûÀ» °¡ÇÔ
        }


        Invoke("OffDamaged", 1); // ¹«Àû½Ã°£ ÇØÁ¦
    }
    private void OnDamaged1(Vector2 targetPos)//ÀûÇÑÅ× È÷Æ®½Ã 
    {
        rigid.velocity = Vector2.zero; // ±âÁ¸ ¼Óµµ¸¦ ÃÊ±âÈ­
=======
            rigid.AddForce(new Vector2(0.5f, 1) * 4, ForceMode2D.Impulse); // ë°˜ëŒ€ ë°©í–¥ìœ¼ë¡œ ë°€ì–´ë‚´ê¸°
        }
        else
        {
            rigid.AddForce(new Vector2(-0.5f, 1) * 4, ForceMode2D.Impulse); // ë°˜ëŒ€ ë°©í–¥ìœ¼ë¡œ ë°€ì–´ë‚´ê¸°
        }

        Invoke("OffDamaged", 1); // ì¼ì • ì‹œê°„ í›„ í”¼í•´ ìƒíƒœ í•´ì œ
    }

    private void OnDamaged1(Vector2 targetPos)
    {
        rigid.velocity = Vector2.zero; // ì´ë™ ì†ë„ ì´ˆê¸°í™”
>>>>>>> Stashed changes

        isDamaged = true; // í”¼í•´ ìƒíƒœ ì„¤ì •

<<<<<<< Updated upstream

        int dirc = rigid.velocityX > 0 ? 1 : -1;

        
        rigid.AddForce(new Vector2(dirc, 1) * 4, ForceMode2D.Impulse); // È÷Æ® ½Ã ÆÃ°Ü³ª°¡´Â ÈûÀ» °¡ÇÔ

        
=======
        int dirc = rigid.velocity.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 4, ForceMode2D.Impulse); // ë°˜ëŒ€ ë°©í–¥ìœ¼ë¡œ ë°€ì–´ë‚´ê¸°
>>>>>>> Stashed changes
    }

    private void Stamina(int Sta)
    {
        UserInterface.GetDamage(Sta);
    }

    void OffDamaged()
    {
<<<<<<< Updated upstream
        gameObject.layer = 10;//layer ¹Ù²Ù±â

        spriteRenderer.color = new Color(1, 1, 1, 1);
=======
        isDamaged = false; // í”¼í•´ ìƒíƒœ í•´ì œ
        gameObject.layer = 10; // layer ë³€ê²½
        spriteRenderer.color = new Color(1, 1, 1, 1); // ìŠ¤í”„ë¼ì´íŠ¸ ì›ë˜ëŒ€ë¡œ ëŒë¦¬ê¸°
>>>>>>> Stashed changes
    }
}
