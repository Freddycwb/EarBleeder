using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MatchCamera : MonoBehaviour
{
    [SerializeField] private GameObjectListVariable players;
    [SerializeField] private GameObjectListVariable projectiles;

    [SerializeField] private float sizeSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distance;
    private float _zoom;

    private Camera _camera;

    private float _y;
    private float _z;

    private Transform _playerXA;
    private Transform _playerXB;
    private Transform _playerZA;
    private Transform _playerZB;

    void Start()
    {
        _camera = GetComponent<Camera>();

        _y = transform.position.y;
        _z = transform.position.z;
    }

    private void Update()
    {
        Vector3 total = Vector3.zero;
        for (int i = 0; i < players.Value.Count; i++)
        {
            total += players.Value[i].activeSelf ? players.Value[i].transform.position : Vector3.zero;
        }
        for (int i = 0; i < projectiles.Value.Count; i++)
        {
            total += projectiles.Value[i].transform.position;
        }

        Vector3 average = total / (players.Value.Count + 1);
        float farthest = 0;

        for (int i = 0; i < players.Value.Count; i++)
        {
            if ((players.Value[i].transform.position - average).magnitude > farthest && players.Value[i].activeSelf)
            {
                farthest = (players.Value[i].transform.position - average).magnitude;
            }
        }
        for (int i = 0; i < projectiles.Value.Count; i++)
        {
            if ((projectiles.Value[i].transform.position - average).magnitude > farthest)
            {
                farthest = (projectiles.Value[i].transform.position - average).magnitude;
            }
        }

        _zoom = Mathf.Lerp(_zoom, (distance / 10 * farthest) + distance,  Time.deltaTime * sizeSpeed);
        transform.position = Vector3.Slerp(transform.position, average + new Vector3(0, _zoom, -_zoom), Time.deltaTime * moveSpeed);
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, new Vector3(45, 0, 0), Time.deltaTime * moveSpeed);
    }
}
