using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    private IInput _input;
    private Rigidbody _rb;
    [SerializeField] private GameObject health;

    [SerializeField] private IntVariable lastIdScored;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAccel;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float dashForce;
    [SerializeField] private float dashHeight;
    [SerializeField] private float dashDelay;
    private float _currentDashTime;

    private GameObject _projectile;
    [SerializeField] private Transform projectileSpawnPoint;
    private bool _shooting;

    [SerializeField] private GameObject meleeAttackObject;
    [SerializeField] private float meleeAttackTime;
    private bool _attacking;

    [SerializeField] private InvokeAfterCollision healthCollider;

    private void OnEnable()
    {
        _shooting = false;
        _attacking = false;
        meleeAttackObject.SetActive(false);
    }

    private void Start()
    {
        _input = GetComponent<IInput>();
        _rb = GetComponent<Rigidbody>();
    }

    public void ResetState()
    {
        _shooting = false;
        transform.eulerAngles = Vector3.back;
        _projectile = transform.GetChild(1).GetChild(0).GetComponentInChildren<SkinObjects>().projectile;
    }

    private void FixedUpdate()
    {
        HorizontalMove();
        MoveRotate();
    }

    private void Update()
    {
        Dash();
        MeleeAttack();

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
        Vector3 goalVel = _shooting || _attacking  ? Vector3.zero : new Vector3(_input.direction.normalized.x, 0, _input.direction.normalized.y) * maxSpeed;
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
        PlayerInput p = Instantiate(_projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation).GetComponent<PlayerInput>();
        p.SetID(_input.id);
    }

    private IEnumerator MeleeAttackCoroutine()
    {
        _attacking = true;
        health.layer = 9;
        meleeAttackObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        meleeAttackObject.SetActive(false);
        health.layer = 6;

        yield return new WaitForSeconds(meleeAttackTime);

        _attacking = false;
    }

    private void MeleeAttack()
    {
        if (_input.bButtonDown && !_attacking)
        {
            StartCoroutine("MeleeAttackCoroutine");
        }
    }


    private void Dash()
    {
        if (_input.aButtonDown && _currentDashTime <= 0)
        {
            health.layer = 9;
            _rb.velocity = Vector3.zero;
            _currentDashTime = dashDelay;
            Vector3 dir = new Vector3(transform.forward.x, dashHeight, transform.forward.z);
            _rb.AddForce(dir * dashForce, ForceMode.Impulse);
        }
        if (_currentDashTime > 0)
        {
            
            _currentDashTime -= Time.deltaTime;
        }
        else
        {
            health.layer = 6;
        }
    }

    public void SetLastIdScored()
    {
        if (healthCollider.GetLastCollision() == null || healthCollider.GetLastCollision().GetComponentInParent<PlayerInput>() == null) return;
        lastIdScored.Value = healthCollider.GetLastCollision().GetComponentInParent<PlayerInput>().id == _input.id ?
            -2 : healthCollider.GetLastCollision().GetComponentInParent<IInput>().id;
    }
}
