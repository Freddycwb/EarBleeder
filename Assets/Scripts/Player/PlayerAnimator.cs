using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private IInput _input;
    private Rigidbody _rb;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject[] limbs;
    private Animator _bodyAnimator;

    [SerializeField] private ParticleSystem notesCircle;
    [SerializeField] private ParticleSystem pingParticle;
    private bool _isPinging;

    private void OnEnable()
    {
        _isPinging = false;
    }

    private void Start()
    {
        _input = GetComponentInParent<PlayerInput>();
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
        HoldingShotParticles();
    }

    private void SetAnimatorVariable()
    {
        _bodyAnimator.SetBool("isMoving", _rb.velocity.magnitude > 0.5);
    }

    private void HoldingShotParticles()
    {
        if (_input.fireButton)
        {
            notesCircle.startColor = Color.yellow;
        }
        else
        {
            notesCircle.startColor = new Color(0, 0, 0, 0);
        }
        if (_input.fireButton && !_isPinging)
        {
            StartCoroutine("DelayBetweenPings");
        }
    }

    private IEnumerator DelayBetweenPings()
    {
        _isPinging = true;
        pingParticle.Play();
        yield return new WaitForSeconds(1);
        _isPinging = false;
    }
}
