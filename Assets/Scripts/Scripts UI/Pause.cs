using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    [SerializeField] private BoolVariable isPaused;
    [SerializeField] private FloatVariable timeScale;
    [SerializeField] private IntVariable playerWhoPaused;
    private PlayerInput _input;

    private bool _canPause = true;
    private CursorLockMode lastCursorVisibility;

    public Action paused;
    public Action unpaused;

    private void Start()
    {
        isPaused.Value = false;
    }

    private void Update()
    {
        if (_input != null && _input.startButtonDown)
        {
            Resume();
        }
    }

    public void PlayerPaused()
    {
        _input = gameObject.AddComponent<PlayerInput>();
        _input.SetID(playerWhoPaused.Value);
        Stop();
    }

    public void Resume()
    {
        if (_input != null)
        {
            Destroy(_input);
        }
        isPaused.Value = false;
        Cursor.lockState = lastCursorVisibility;
        Time.timeScale = timeScale.Value;
        unpaused.Invoke();
    }

    private void Stop()
    {
        isPaused.Value = true;
        lastCursorVisibility = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        paused.Invoke();
    }
}
