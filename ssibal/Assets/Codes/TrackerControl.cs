using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerControl : MonoBehaviour
{
    public bool isTracing = false;
    public GameObject traceobject;


    [Range(1, 10)]
    public float traceSpeed = 5;
    private Rigidbody2D rb;
    private SpriteRenderer spr;


    [SerializeField][Range(1, 5)] private float apearSpeed;
    private float color_a = 0;
    private bool isApear = false;
    private Color myColor;

    #region Unity_Function
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        myColor = spr.color;
        spr.color = new Color(myColor.r, myColor.g, myColor.b, color_a);
    }
    private void Update()
    {
        Trace();
    }

    #endregion

    #region Function
    private void Trace()
    {
        if (isTracing && traceobject != null)
        {
            Vector3 targetPos = traceobject.transform.position;
            Vector3 myPos = transform.position;

            Vector3 direction = (targetPos - myPos).normalized;

            rb.velocity = new Vector2(direction.x * traceSpeed, rb.velocity.y);

            spr.flipX = direction.x > 0 ? false : true;
            Apear();
        }
    }

    private void Apear()
    {
        if (!isApear)
        {
            isApear = true;
            StartCoroutine(_Apear());
        }
    }
    private IEnumerator _Apear()
    {
        while (color_a <= 1)
        {
            color_a += Time.deltaTime * apearSpeed;
            spr.color = new Color(myColor.r, myColor.g, myColor.b, color_a);
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
}
