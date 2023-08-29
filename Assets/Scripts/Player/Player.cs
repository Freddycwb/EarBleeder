using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    private IInput _input;
    private Rigidbody _rb;

    [SerializeField] private IntVariable lastIdScored;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAccel;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float dashForce;
    [SerializeField] private float dashHeight;
    [SerializeField] private float dashDelay;
    private float _currentDashTime;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPoint;
    private bool _shooting;

    [SerializeField] private InvokeAfterCollision healthCollider;

    private void OnEnable()
    {
        _shooting = false;
    }

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
        Dash();

        if (_input.fireButtonDown)
        {
            Fire();
            _shooting = true;
        }
        else if (_input.fireButtonUp)
        {
            _shooting = false;
        }
    }

    private void HorizontalMove()
    {
        Vector3 goalVel = _shooting ? Vector3.zero : new Vector3(_input.direction.normalized.x, 0, _input.direction.normalized.y) * maxSpeed;
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
        PlayerInput p = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation).GetComponent<PlayerInput>();
        p.SetID(GetComponent<PlayerInput>().GetID());
    }

    private void Dash()
    {
        if (_input.aButtonDown && _currentDashTime <= 0)
        {
            _rb.velocity = Vector3.zero;
            _currentDashTime = dashDelay;
            Vector3 dir = new Vector3(transform.forward.x, dashHeight, transform.forward.z);
            _rb.AddForce(dir * dashForce, ForceMode.Impulse);
        }
        if (_currentDashTime > 0)
        {
            _currentDashTime -= Time.deltaTime;
        }
    }

    public void SetLastIdScored()
    {
        if (healthCollider.GetLastCollision().GetComponentInParent<PlayerInput>() == null) return;
        lastIdScored.Value = healthCollider.GetLastCollision().GetComponentInParent<PlayerInput>().GetID() == GetComponent<PlayerInput>().GetID() ?
            -2 : healthCollider.GetLastCollision().GetComponentInParent<PlayerInput>().GetID();
    }
}
