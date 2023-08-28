using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private GameObject body;
    private Animator _bodyAnimator;

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _bodyAnimator = body.GetComponent<Animator>();
    }

    public void SetBody(GameObject skin)
    {
        if (body.transform.childCount > 0)
        {
            Destroy(body.transform.GetChild(0).gameObject);
        }
        GameObject bodyNewChild = Instantiate(skin, body.transform.position, body.transform.rotation);
        bodyNewChild.transform.SetParent(body.transform);
    }

    private void Update()
    {
        SetAnimatorVariable();
    }

    private void SetAnimatorVariable()
    {
        _bodyAnimator.SetBool("isMoving", _rb.velocity.magnitude > 0.5);
    }
}
