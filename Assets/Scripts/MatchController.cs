using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchController : MonoBehaviour
{
    [SerializeField] private IntVariable controlsNumber;
    [SerializeField] private IntVariable playersNumber;

    private List<PlayerInput> controlsNotPlaying = new List<PlayerInput>();
    [SerializeField] private PlayerSlot[] playerSlots;

    private void Awake()
    {
        Actions();
    }

    private void Actions()
    {
        foreach (PlayerSlot slot in playerSlots)
        {
            slot.leave += PlayerLeave;
        }
    }

    void Start()
    {
        SetStartVariables();
        for (int i = 0; i < NumberOfJoysticks(); i++)
        {
            PlayerConnected(i);
        }
        StartCoroutine("CheckReadyAndNewJoystick");
    }

    private void SetStartVariables()
    {
        playersNumber.Value = 0;
        controlsNotPlaying.Add(GetComponent<PlayerInput>());
        playerSlots[0].enabled = true;
        controlsNumber.Value = 1;
    }

    private int NumberOfJoysticks()
    {
        int n = 0;
        string[] temp = Input.GetJoystickNames();

        if (temp.Length > 0)
        {
            for (int i = 0; i < temp.Length; ++i)
            {
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    n++;
                }
            }
        }

        return n;
    }

    private IEnumerator CheckReadyAndNewJoystick()
    {
        yield return new WaitForSeconds(1);

        ConnectAndDesconnect();
        CheckReady();
    }

    private void ConnectAndDesconnect()
    {
        // have this + 1 and - 1 in this function because it have to count and ignore the keyboard input
        if (controlsNumber.Value < NumberOfJoysticks() + 1)
        {
            for (int i = controlsNumber.Value; i < NumberOfJoysticks() + 1; i++)
            {
                PlayerConnected(i - 1);
            }
        }
        else if (controlsNumber.Value > NumberOfJoysticks() + 1)
        {
            for (int i = controlsNumber.Value; i > NumberOfJoysticks() + 1; i--)
            {
                PlayerDesconnected(i - 1);
            }
        }
    }

    private void CheckReady()
    {
        bool everyoneReady = false;
        bool atLastOneReady = false;
        bool atLastOneIsntReady = false;
        foreach (PlayerSlot slot in playerSlots)
        {
            if (slot.enabled && slot.GetInput() != null && slot.GetReady())
            {
                atLastOneReady = true;
            }
            if (slot.enabled && slot.GetInput() != null && !slot.GetReady())
            {
                atLastOneIsntReady = true;
            }
        }
        if (atLastOneReady && !atLastOneIsntReady)
        {
            everyoneReady = true;
        }
        if (everyoneReady)
        {
            foreach (PlayerSlot slot in playerSlots)
            {
                if (slot.enabled)
                {
                    slot.FreePlayer();
                }
                else
                {
                    slot.gameObject.SetActive(false);
                }
            }
            StopAllCoroutines();
        }
        else
        {
            StartCoroutine("CheckReadyAndNewJoystick");
        }
    }

    private void Update()
    {
        for (int i = 0; i < controlsNotPlaying.Count; i++)
        {
            if (controlsNotPlaying[i].aButtonUp)
            {
                PlayerJoin(i);
            }
        }
    }

    private void PlayerConnected(int i)
    {
        PlayerInput p = gameObject.AddComponent<PlayerInput>();
        p.SetID(i);
        controlsNotPlaying.Add(p);
        controlsNumber.Value++;
        playerSlots[controlsNumber.Value - 1].enabled = true;
    }

    private void PlayerJoin(int i)
    {
        playerSlots[playersNumber.Value].SetInput(controlsNotPlaying[i].GetID());
        playersNumber.Value++;
        Destroy(controlsNotPlaying[i]);
        controlsNotPlaying.RemoveAt(i);
    }

    private void PlayerLeave(int inputID)
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i].GetInputID() == inputID)
            {
                playerSlots[i].Leave();
            }
        }
        PlayerInput pi = gameObject.AddComponent<PlayerInput>();
        pi.SetID(inputID);
        playersNumber.Value--;
        controlsNotPlaying.Add(pi);
        ArrangeSlots();
    }

    private void PlayerDesconnected(int i)
    {
        controlsNumber.Value--;
        Debug.Log(Mathf.Clamp(controlsNumber.Value - 1, 1, playerSlots.Length - 1));
        playerSlots[Mathf.Clamp(controlsNumber.Value - 1, 1, playerSlots.Length - 1)].enabled = false;
        foreach (PlayerInput p in controlsNotPlaying)
        {
            if (p.GetID() + 1 == i)
            {
                Destroy(p);
                controlsNotPlaying.Remove(p);
                Debug.Log("Desconectou");
                return;
            }
        }
    }

    private void ArrangeSlots()
    {
        List<int> inputsIDs = new List<int>();
        List<int> skins = new List<int>();
        List<bool> readys = new List<bool>();
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i].GetInput() != null)
            {
                inputsIDs.Add(playerSlots[i].GetInputID());
                skins.Add(playerSlots[i].GetSkin());
                readys.Add(playerSlots[i].GetReady());
                playerSlots[i].Leave();
            }
        }
        if (inputsIDs.Count > 0)
        {
            for (int i = 0; i < controlsNumber.Value - 1; i++)
            {
                playerSlots[i].SetInput(inputsIDs[0]);
                playerSlots[i].SetSkin(skins[0]);
                playerSlots[i].SetReady(readys[0]);
                inputsIDs.RemoveAt(0);
                skins.RemoveAt(0);
                readys.RemoveAt(0);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (PlayerSlot slot in playerSlots)
        {
            slot.leave -= PlayerLeave;
        }
    }
}
