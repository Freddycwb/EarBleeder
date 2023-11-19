using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsHP : MonoBehaviour
{
    [SerializeField] private InvokeAfter deathInvoke;
    [SerializeField] private float timeToDie;
    [SerializeField] private ParticleSystem smoke;
    private float _currentTimeToDie;
    private bool _outOfBounds;

    private void OnEnable()
    {
        _outOfBounds = false;
        smoke.Stop();
    }

    public void SetOutOfBounds(bool value)
    {
        _outOfBounds = value;
        if (value && gameObject.layer == 6)
        {
            smoke.Play();
        }
        else
        {
            smoke.Stop();
        }
    }

    void Update()
    {
        _currentTimeToDie += _outOfBounds && gameObject.layer == 6 ? Time.deltaTime : -Time.deltaTime;
        _currentTimeToDie = Mathf.Clamp(_currentTimeToDie, 0, timeToDie);
        if (_currentTimeToDie >= timeToDie)
        {
            deathInvoke.CallAction();
        }
    }
}
