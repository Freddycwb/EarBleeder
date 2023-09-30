using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private IInput _input;
    private Rigidbody _rb;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject[] arms;
    [SerializeField] private GameObject[] legs;
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
        int i = 0;
        foreach (GameObject newArms in arms)
        {
            newArms.GetComponent<MeshRenderer>().material = limbsMaterial;
            if (bodyNewChild.GetComponent<SkinObjects>().arms.Length > 0)
            {
                arms[i].GetComponent<MeshRenderer>().enabled = false;
                GameObject newArm = Instantiate(bodyNewChild.GetComponent<SkinObjects>().arms[i], arms[i].transform.position, arms[i].transform.rotation);
                newArm.transform.SetParent(arms[i].transform);
            }
            else
            {
                arms[i].GetComponent<MeshRenderer>().enabled = true;
                if (arms[i].transform.childCount > 0)
                {
                    Destroy(arms[i].transform.GetChild(0).gameObject);
                }
            }
            i++;
        }

        foreach (GameObject legs in legs)
        {
            legs.GetComponent<MeshRenderer>().material = limbsMaterial;
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
