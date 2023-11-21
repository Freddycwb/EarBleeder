using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWallsPosition : MonoBehaviour
{
    private Animator _animator;
    private bool switched;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchWallPos()
    {
        if(switched)
        {
            _animator.Play("MovingDoor");
        }
        else
        {
            _animator.Play("MovingDoor2");
        }

        switched = !switched;
    }
}
