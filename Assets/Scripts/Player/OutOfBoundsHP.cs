using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsHP : MonoBehaviour
{
    [SerializeField] private InvokeAfter deathInvoke;
    [SerializeField] private float timeToDie;
    private float _currentTimeToDie;
    private bool _outOfBounds;

    private void OnEnable()
    {
        _outOfBounds = false;
    }

    public void SetOutOfBounds(bool value)
    {
        _outOfBounds = value;
    }

    void Update()
    {
        _currentTimeToDie += _outOfBounds ? Time.deltaTime : -Time.deltaTime;
        _currentTimeToDie = Mathf.Clamp(_currentTimeToDie, 0, timeToDie);
        if (_currentTimeToDie >= timeToDie)
        {
            deathInvoke.CallAction();
        }
    }
}
