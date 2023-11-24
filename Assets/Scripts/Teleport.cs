using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public GameObject particle;
    public GameObject sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(particle, other.transform.position, transform.rotation);
            other.transform.position = destination.position;
            Instantiate(particle, other.transform.position, transform.rotation);
            Instantiate(sound, other.transform.position, transform.rotation);
        }
    }
}




