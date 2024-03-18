using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    float jumpPower;
    bool isJump = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        Jump();
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name.Equals("Ground"))
        {
            isJump = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate() 
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0){
            spriter.flipX = inputVec.x < 0;
        }
    }
    
}
