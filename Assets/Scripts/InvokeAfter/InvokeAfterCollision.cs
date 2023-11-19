using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterCollision : InvokeAfter
{
    [SerializeField] private List<string> tags = new List<string>();
    [SerializeField] private bool checkOnEnableOnly;
    private GameObject lastCollision;

    public GameObject GetLastCollision()
    {
        return lastCollision;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tags.Count == 0 || tags.Contains(other.tag))
        {
            if (!checkOnEnableOnly || (checkOnEnableOnly && enabled))
            {
                lastCollision = other.gameObject;
                CallAction();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tags.Count == 0 || tags.Contains(other.tag))
        {
            if (!checkOnEnableOnly || (checkOnEnableOnly && enabled))
            {
                CallSubAction();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (tags.Count == 0 || tags.Contains(other.gameObject.tag))
        {
            if (!checkOnEnableOnly || (checkOnEnableOnly && enabled))
            {
                lastCollision = other.gameObject;
                CallAction();
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (tags.Count == 0 || tags.Contains(other.gameObject.tag))
        {
            if (!checkOnEnableOnly || (checkOnEnableOnly && enabled))
            {
                CallSubAction();
            }
        }
    }
}
