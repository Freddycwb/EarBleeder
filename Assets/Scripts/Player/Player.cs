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
    [SerializeField] private float rotateSpeed;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPoint;

    private void Start()
    {
        _input = GetComponent<IInput>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HorizontalMove();
        MoveRotate();
    }

    private void Update()
    {
        if (_input.fireButtonDown)
        {
            Fire();
        }
    }

    private void HorizontalMove()
    {
        Vector3 goalVel = new Vector3(_input.direction.normalized.x, 0, _input.direction.normalized.y) * maxSpeed;
        Vector3 neededAccel = goalVel - _rb.velocity;
        neededAccel -= Vector3.up * neededAccel.y;
        neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
        _rb.AddForce(neededAccel, ForceMode.Impulse);
    }

    private void MoveRotate()
    {
        if (_input.direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(_input.direction.x, 0, _input.direction.y)), Time.deltaTime * rotateSpeed);
        }
    }

    private void Fire()
    {
        Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }
}
