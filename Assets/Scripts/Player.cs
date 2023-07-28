using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    private IInput _input;
    private Rigidbody _rb;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAccel;

    private void Start()
    {
        _input = GetComponent<IInput>();
        _rb = GetComponent<Rigidbody>();
        //teste
    }

    private void FixedUpdate()
    {
        HorizontalMove();
    }

    void HorizontalMove()
    {
        Vector3 goalVel = new Vector3(_input.direction.normalized.x, 0, _input.direction.normalized.y) * maxSpeed;
        Vector3 neededAccel = goalVel - _rb.velocity;
        neededAccel -= Vector3.up * neededAccel.y;
        neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
        _rb.AddForce(neededAccel, ForceMode.Impulse);
    }
}
