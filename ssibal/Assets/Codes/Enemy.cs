using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int nextMove;
    private Rigidbody2D rigid;
    private bool isMove = false;
    private SpriteRenderer spriter;
    private Animator anim;

    private CapsuleCollider2D cscd;

    #region Unity_Function
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cscd = GetComponent<CapsuleCollider2D>();
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
            nextMove = Random.Range(1, -2);
            yield return new WaitForSeconds(3);
            _Think();
        }
    }

    private void _DrawRay()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rayHit.collider == null) {
            StopAllCoroutines();
            _Turn();
        }
    }

    private void _Turn()
    {
        StartCoroutine(turn());
        IEnumerator turn()
        {
        nextMove *= -1;
        yield return new WaitForSeconds(3);
        _Think();
        yield break;
        }
    }

      private void _OnDie()
    {
        StartCoroutine(OnDie());
        IEnumerator OnDie()
        {
        spriter.color = new Color(1, 1, 1, 0.4f);
        spriter.flipY = true;
        cscd.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        yield return new WaitForSeconds(5);
        _DeActive();
        yield break;
        }
    }

     private void _DeActive()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
