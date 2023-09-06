using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject[] limbs;
    private Animator _bodyAnimator;

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _bodyAnimator = body.GetComponent<Animator>();
    }

    public void SetBody(GameObject skin)
    {
        if (body.transform.childCount > 4)
        {
            Destroy(body.transform.GetChild(body.transform.childCount - 1).gameObject);
        }
        GameObject bodyNewChild = Instantiate(skin, body.transform.position, body.transform.rotation);
        bodyNewChild.transform.SetParent(body.transform);
        bodyNewChild.GetComponent<PlayerInput>().SetID(GetComponentInParent<IInput>().id);
        Material limbsMaterial = bodyNewChild.GetComponent<SkinObjects>().limbsMaterial;
        foreach (GameObject limb in limbs)
        {
            limb.GetComponent<MeshRenderer>().material = limbsMaterial;
        }
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
