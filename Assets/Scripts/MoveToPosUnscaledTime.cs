using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosUnscaledTime : MonoBehaviour
{
    [SerializeField] private Vector3 pos;
    [SerializeField] private float moveVel;
    [SerializeField] private bool local;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position != pos)
        {
            if (!local)
            {
                transform.position = Vector3.Slerp(transform.position, pos, Time.unscaledDeltaTime * moveVel);
            }
            else
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, pos, Time.unscaledDeltaTime * moveVel);
            }
        }
    }
}
