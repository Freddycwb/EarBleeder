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
    [SerializeField] private float dashInvincibleTime;
    [SerializeField] private float dashDelay;
    private bool _dashing;

    private GameObject _projectile;
    [SerializeField] private Transform projectileSpawnPoint;
    private bool _shooting;

    [SerializeField] private GameObject meleeAttackObject;
    [SerializeField] private float meleeAttackTime;
    [SerializeField] private float meleeAttackDelay;
    private bool _attacking;

    [SerializeField] private InvokeAfterCollision healthCollider;

    [SerializeField] private IntVariable playerWhoPaused;
    [SerializeField] private GameEvent pauseWasPressed;
    [SerializeField] private BoolVariable isPaused;

    private void OnEnable()
    {
        _dashing = false;
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
        if (isPaused.Value) { return; }
        HorizontalMove();
        MoveRotate();
    }

    private void Update()
    {
        Pause();
        if (isPaused.Value) { return; }
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
        Vector3 goalVel = _shooting || _attacking || _dashing ? Vector3.zero : new Vector3(_input.direction.normalized.x, 0, _input.direction.normalized.y) * maxSpeed;
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

        yield return new WaitForSeconds(meleeAttackTime);

        meleeAttackObject.SetActive(false);
        health.layer = 6;

        yield return new WaitForSeconds(meleeAttackDelay);

        _attacking = false;
    }

    private void MeleeAttack()
    {
        if (_input.bButtonDown && !_attacking)
        {
            StartCoroutine("MeleeAttackCoroutine");
        }
    }

    private void Pause()
    {
        if (_input.startButtonDown)
        {
            playerWhoPaused.Value = _input.id;
            pauseWasPressed.Raise();
        }
    }

    private IEnumerator DashCoroutine()
    {
        _dashing = true;
        health.layer = 9;

        _rb.velocity = Vector3.zero;
        Vector3 dir = new Vector3(transform.forward.x, dashHeight, transform.forward.z);
        _rb.AddForce(dir * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(dashInvincibleTime);

        health.layer = 6;

        yield return new WaitForSeconds(dashDelay);

        _dashing = false;
    }

    private void Dash()
    {
        if (_input.aButtonDown && !_dashing)
        {
            StartCoroutine("DashCoroutine");
        }
    }

    public void SetLastIdScored()
    {
        if (healthCollider.GetLastCollision() == null || healthCollider.GetLastCollision().GetComponentInParent<PlayerInput>() == null) return;
        lastIdScored.Value = healthCollider.GetLastCollision().GetComponentInParent<PlayerInput>().id == _input.id ?
            -2 : healthCollider.GetLastCollision().GetComponentInParent<IInput>().id;
    }
}
