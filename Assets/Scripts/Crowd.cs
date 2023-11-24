using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    [SerializeField] private Vector2 randomMoveDist;
    [SerializeField] private Material[] characterMaterials;
    [SerializeField] private GameObject[] sticks;
    [SerializeField] private Material[] sticksMaterials;

    void Start()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = characterMaterials[Random.Range(0, characterMaterials.Length)];
        transform.position = new Vector3(transform.position.x + Random.Range(-randomMoveDist.x, randomMoveDist.x), transform.position.y, transform.position.z + Random.Range(-randomMoveDist.y, randomMoveDist.y));

        int sticksNumber = Random.Range(0, 10);

        if (sticksNumber == 0)
        {
            sticks[0].SetActive(true);
            sticks[0].GetComponent<MeshRenderer>().material = sticksMaterials[Random.Range(0, sticksMaterials.Length)];
        }
        else if (sticksNumber == 1)
        {
            sticks[1].SetActive(true);
            sticks[1].GetComponent<MeshRenderer>().material = sticksMaterials[Random.Range(0, sticksMaterials.Length)];
        }
        else if (sticksNumber == 2)
        {
            sticks[0].SetActive(true);
            sticks[0].GetComponent<MeshRenderer>().material = sticksMaterials[Random.Range(0, sticksMaterials.Length)];
            sticks[1].SetActive(true);
            sticks[1].GetComponent<MeshRenderer>().material = sticksMaterials[Random.Range(0, sticksMaterials.Length)];
        }
    }
}
