using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using JetBrains.Rider.Unity.Editor;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Hp;
    public Vector2 inputVec;
    public float speed;
    public float jumpPower;

    private bool canJump = false;
    private bool isMove = false;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;

    #region Unity_Function
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        _Jump();
        _Move();
        _Anim();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground") canJump = true;
    }
    #endregion

    #region Function
    private void _Move()
    {
        float h = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);

        if(h != 0) isMove = true;
        else isMove = false;
    }
    private void _Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void _Anim()
    {
        anim.SetBool("Move", isMove);
        
        if (rigid.velocity.x < 0) spriter.flipX = true;
        else spriter.flipX = false;
    }
    #endregion
}
