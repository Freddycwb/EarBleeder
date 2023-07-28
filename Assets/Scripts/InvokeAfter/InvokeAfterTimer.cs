using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfterTimer : InvokeAfter
{
    [SerializeField] private float timeToAction;
    [SerializeField] private bool desableAfterTimer = true;

    private void OnEnable()
    {
        if (timeToAction > 0)
        {
            StartCoroutine("InvokeAfterSeconds");
        }
    }

    private IEnumerator InvokeAfterSeconds()
    {
        yield return new WaitForSeconds(timeToAction);
        CallAction();
        enabled = !desableAfterTimer;
    }

    public void SetTimeToAction(float time)
    {
        timeToAction = time;
        StartCoroutine("InvokeAfterSeconds");
    }
}
