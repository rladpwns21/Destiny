using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private TrackerControl trackercontrol;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            trackercontrol.isTracing = true;
            trackercontrol.traceobject = other.gameObject;
        }
    }
}
