using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center,size);
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = new Vector3(transform.position.x, 1.5f, -10f);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - width;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
