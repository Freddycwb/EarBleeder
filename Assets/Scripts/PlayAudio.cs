using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayAudio : MonoBehaviour
{
    public string AEvent;
    public string BEvent;
    public string fireEvent;

    private IInput _input;

    void Start()
    {
        _input = GetComponent<IInput>();
    }

    private void Update()
    {
        if (_input.aButtonDown)
        {
            RuntimeManager.PlayOneShot(AEvent, transform.position);
        }
        if (_input.bButtonDown)
        {
            RuntimeManager.PlayOneShot(BEvent, transform.position);
        }
        if (_input.fireButtonDown)
        {
            RuntimeManager.PlayOneShot(fireEvent, transform.position);
        }
    }
}
