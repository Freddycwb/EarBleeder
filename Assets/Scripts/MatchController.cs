using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchController : MonoBehaviour
{
    [SerializeField] private IntVariable playersNumber;
    [SerializeField] private GameObject player;

    void Start()
    {
        playersNumber.Value = 0;
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            GameObject p = Instantiate(player, transform.position, transform.rotation);
            p.GetComponent<PlayerInput>().SetID(i);
            playersNumber.Value++;
        }
    }
}
