using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerSlot : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private IntVariable controlsNumber;
    private int playerSlotID;

    [SerializeField] private float range;

    private void Start()
    {
        playerSlotID = GetComponent<PlayerSlot>().GetPlayerSlotID();
    }

    void Update()
    {
        MoveSlot();
    }

    private void MoveSlot()
    {
        float x = -range / 2 + (playerSlotID * range) / (controlsNumber.Value + 1);
        transform.position =  Vector3.Slerp(transform.position, new Vector3(x, 0, -3), Time.time * speed * Time.deltaTime);
    }
}
