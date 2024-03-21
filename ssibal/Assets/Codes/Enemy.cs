using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int nextMove;
    public float speed;
    private Rigidbody2D rigid;
    private bool isMove = false;
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
        _Move();
        _Anim();
    }

    #endregion

    #region Function
    private void _Move()
    {
        rigid.velocity = new Vector2(speed, rigid.velocity.y);

        if(speed != 0) isMove = true;
        else isMove = false;
    }

    private void _Anim()
    {
        anim.SetBool("Move", isMove);
        
        if (rigid.velocity.x < 0) spriter.flipX = true;
        else spriter.flipX = false;
    }
    private void Think()
    {
        nextMove = Random.Range(1, -2);
    }
    #endregion
}
