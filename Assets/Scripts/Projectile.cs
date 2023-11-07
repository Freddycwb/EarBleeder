using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Projectile : MonoBehaviour
{
    private IInput _input;

    [SerializeField] private GameObjectListVariable projectiles;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minTimeToExplode;
    private float _currentMinTimeToExplode;

    [SerializeField] private GameEvent explosionEvent;

    [SerializeField] private GameObject explosion;

    [SerializeField] private BoolVariable isPaused;

    private void Start()
    {
        _input = GetComponent<IInput>();
        projectiles.Value.Add(gameObject);
        _currentMinTimeToExplode = minTimeToExplode;
    }

    void Update()
    {
        if (isPaused.Value) { return; }
        HorizontalMove();
        if (!_input.fireButton && _currentMinTimeToExplode < 0)
        {
            Explosion();
        }
        else
        {
            _currentMinTimeToExplode -= Time.deltaTime;
        }
    }

    private void HorizontalMove()
    {
        if (_input.direction.magnitude > 0.5f)
        {
            transform.rotation = transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(_input.direction.x, 0, _input.direction.y)), Time.deltaTime * rotationSpeed);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    public void Explosion()
    {
        explosionEvent.Raise();
        PlayerInput e = Instantiate(explosion, transform.position, transform.rotation).GetComponent<PlayerInput>();
        e.SetID(_input.id);
        projectiles.Value.Remove(gameObject);
        Destroy(gameObject);
    }
}
