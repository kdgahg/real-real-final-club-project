using UnityEngine;

public class Slime : MonoBehaviour
{
    Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(-3,rigid.velocity.y);
    }
}
