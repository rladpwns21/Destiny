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


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Trace();
    }

    private void Trace()
    {
        if (isTracing && traceobject != null)
        {
            Vector3 targetPos = traceobject.transform.position;
            Vector3 myPos = transform.position;

            Vector3 direction = (targetPos - myPos).normalized;

            rb.velocity = new Vector2(direction.x * traceSpeed, rb.velocity.y);

            spr.flipX = direction.x > 0 ? false : true;

            Debug.Log(direction.x * traceSpeed);
        }
    }

}
