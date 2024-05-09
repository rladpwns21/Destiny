using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using JetBrains.Rider.Unity.Editor;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject WeaponCollider;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private CapsuleCollider2D cscd;
    private Animator anim;
    public int Hp;
    public float speed;
    public float jumpPower;
    private bool canJump = false;
    private bool isMove = false;
    private bool canMove = true;


    #region Unity_Function
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cscd = GetComponent<CapsuleCollider2D>();
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
        if (other.gameObject.tag == "Enemy")
        {
            _OnDamaged(other.transform.position);
        }

    }
    #endregion

    #region Function
    private void _Move()
    {
        if (canMove)
        {
            float h = Input.GetAxis("Horizontal");

            rigid.velocity = new Vector2(h * speed, rigid.velocity.y);

            if (h != 0) isMove = true;
            else isMove = false;
        }
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

    private void _OnDamaged(Vector2 targetPos)
    {
        StartCoroutine(OnDamage());
        IEnumerator OnDamage()
        {
            GameManager.instance._HpDown();
            gameObject.layer = 13;
            spriter.color = new Color(1, 1, 1, 0.4f);

            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1f) * 4, ForceMode2D.Impulse);

            canMove = false;

            yield return new WaitForSeconds(1f);

            canMove = true;

            _OffDamaged();

            yield break;
        }

    }

    private void _OffDamaged()
    {
        gameObject.layer = 12;
        spriter.color = new Color(1, 1, 1, 1);
    }

    public void _OnDie()
    {
        canMove = false;
        canJump = false;
        gameObject.layer = 13;
        anim.SetTrigger("isDie");
    }

    private void WeaponColliderOnOff()
    {
        WeaponCollider.SetActive ( !WeaponCollider.activeInHierarchy );
    }

    public void _Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Attack");
        }
    }
    #endregion
}
