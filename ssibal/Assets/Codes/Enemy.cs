using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int nextMove;
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
        _Think();
    }

    void Update()
    {
        _Move();
        _Anim();
        _DrawRay();
    }

    #endregion

    #region Function
    private void _Move()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        if (nextMove != 0) isMove = true;
        else isMove = false;
    }

    private void _Anim()
    {
        anim.SetBool("Move", isMove);

        if (rigid.velocity.x < 0) spriter.flipX = true;
        else spriter.flipX = false;
    }
    private void _Think()
    {
        StartCoroutine(think());
        IEnumerator think()
        {
            Debug.Log("Random");
            while (true)
            {
                nextMove = Random.Range(1, -2);
                if(nextMove != 0) break;
            }
            yield return new WaitForSeconds(5);
            _Think();
        }
    }

    private void _DrawRay()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null) rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y);
    }
    #endregion
}
