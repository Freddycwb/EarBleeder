using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent pause;
    [SerializeField] private UnityEvent unpause;
    private Pause _pause;

    void Start()
    {
        _pause = GetComponent<Pause>();
        _pause.paused += InvokePauseEvent;
        _pause.unpaused += InvokeUnpauseEvent;
    }

    void InvokePauseEvent()
    {
        pause.Invoke();
    }

    void InvokeUnpauseEvent()
    {
        unpause.Invoke();
    }

    private void OnDestroy()
    {
        if(_pause == null) { return; }
        _pause.paused -= InvokePauseEvent;
        _pause.unpaused -= InvokeUnpauseEvent;
    }
}
