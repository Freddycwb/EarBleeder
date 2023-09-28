using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 0.3f)
            {
                Vector3 newPos = destination.position;
                player.transform.position = newPos;
            }
        }
        else
        {
            return;
        }
    }
}




