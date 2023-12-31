using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchController : MonoBehaviour
{
    [SerializeField] private GameEvent matchStart;
    [SerializeField] private GameEvent matchEnd;
    [SerializeField] private GameEvent roundSetted;
    [SerializeField] private GameEvent roundStart;
    [SerializeField] private GameEvent roundEnd;
    [SerializeField] private GameEvent goMainMenu;

    private bool _endingRound;
    private bool _firstRound;

    [SerializeField] private IntVariable lastIdScored;
    [SerializeField] private IntListVariable playerScores;
    [SerializeField] private int scoreToWin;

    private bool atLobby = true;
    [SerializeField] private IntVariable controlsNumber;
    [SerializeField] private IntVariable playersNumber;

    [SerializeField] private GameObjectListVariable players;

    private List<PlayerInput> controlsNotPlaying = new List<PlayerInput>();
    [SerializeField] private GameObject playerSlot;
    private List<PlayerSlot> _playerSlots = new List<PlayerSlot>();
    private List<PlayerSlot> playerSlotsInGame = new List<PlayerSlot>();

    private CharacterSelection skinSelection;

    [SerializeField] private GameObject currentStage;
    [SerializeField] private GameObjectListVariable stages;

    private bool _matchOver;

    private void OnEnable()
    {
        SetStartVariables();
        CreatePlayerSlot();
        atLobby = true;
        controlsNotPlaying.Clear();
        PlayerInput p = gameObject.AddComponent<PlayerInput>();
        p.SetID(-1);
        controlsNotPlaying.Add(p);
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            PlayerConnected(i);
        }
        StartCoroutine("CheckReadyAndNewJoystick");
        skinSelection = FindObjectOfType<CharacterSelection>();
    }

    private void SetStartVariables()
    {
        lastIdScored.Value = 0;
        playerScores.Value.Clear();
        controlsNumber.Value = 1;
        playersNumber.Value = 0;
        players.Value.Clear();
    }

    private void CreatePlayerSlot()
    {
        if (_playerSlots.Count >= 4)
        {
            return;
        }
        PlayerSlot p = Instantiate(playerSlot, new Vector3(0, 0, -3), playerSlot.transform.rotation).GetComponent<PlayerSlot>();
        p.leave += PlayerLeave;
        p.SetID(controlsNumber.Value);
        _playerSlots.Add(p);
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
        if (controlsNumber.Value < Gamepad.all.Count + 1)
        {
            for (int i = controlsNumber.Value; i < Gamepad.all.Count + 1; i++)
            {
                PlayerConnected(i - 1);
            }
        }
        else if (controlsNumber.Value > Gamepad.all.Count + 1)
        {
            for (int i = controlsNumber.Value; i > Gamepad.all.Count + 1; i--)
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
        foreach (PlayerSlot slot in _playerSlots)
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
            StopAllCoroutines();
            StartMatch();
        }
        else
        {
            StartCoroutine("CheckReadyAndNewJoystick");
        }
    }

    private void Update()
    {
        if (atLobby)
        {
            for (int i = 0; i < controlsNotPlaying.Count; i++)
            {
                if (controlsNotPlaying[i].aButtonUp)
                {
                    PlayerJoin(i);
                }
            }
        }
    }

    private void PlayerConnected(int i)
    {
        PlayerInput p = gameObject.AddComponent<PlayerInput>();
        p.SetID(i);
        controlsNotPlaying.Add(p);
        controlsNumber.Value++;
        CreatePlayerSlot();
        _playerSlots[Mathf.Clamp(controlsNumber.Value - 1, 0, _playerSlots.Count - 1)].enabled = true;
    }

    private void PlayerJoin(int i)
    {
        if (playersNumber.Value > _playerSlots.Count - 1) return;
        _playerSlots[playersNumber.Value].SetInput(controlsNotPlaying[i].id, "Set input on player join on player slot ");
        playersNumber.Value++;
        Destroy(controlsNotPlaying[i]);
        controlsNotPlaying.RemoveAt(i);
    }

    private void PlayerLeave(int inputID)
    {
        for (int i = 0; i <= Mathf.Clamp(controlsNumber.Value - 1, 0, _playerSlots.Count - 1); i++)
        {
            if (_playerSlots[i].GetInputID() == inputID)
            {
                _playerSlots[i].Leave();
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
        PlayerSlot p = _playerSlots[controlsNumber.Value];
        _playerSlots.RemoveAt(controlsNumber.Value);
        p.Disconnected();
        Destroy(p.gameObject);
        foreach (PlayerInput pI in controlsNotPlaying)
        {
            if (pI.id + 1 == i)
            {
                Destroy(pI);
                controlsNotPlaying.Remove(pI);
                ArrangeSlots();
                return;
            }
        }
        ArrangeSlots();
        playersNumber.Value--;
    }

    private void ArrangeSlots()
    {
        List<int> inputsIDs = new List<int>();
        int[] skins = skinSelection.GetSelectorsOverlapping();
        List<bool> readys = new List<bool>();
        int skisOrder = 0;
        for (int i = 0; i < _playerSlots.Count; i++)
        {
            if (_playerSlots[i].GetInput() != null)
            {
                inputsIDs.Add(_playerSlots[i].GetInputID());
                readys.Add(_playerSlots[i].GetReady());
                skins[skisOrder] = skinSelection.GetSelectorOverlapping(_playerSlots[i].GetPlayerSlotID() - 1);
                skisOrder++;
                _playerSlots[i].Disconnected();
            }
        }
        skinSelection.SetSelectorsOverlapping(skins);
        for (int i = 0; i <= Mathf.Clamp(controlsNumber.Value - 1, 0, _playerSlots.Count - 1); i++)
        {
            _playerSlots[i].enabled = true;
        }
        if (inputsIDs.Count > 0)
        {
            for (int i = 0; i < inputsIDs.Count; i++)
            {
                _playerSlots[i].SetInput(inputsIDs[i], "Set input on arrange slots");
                _playerSlots[i].SetSkin();
                _playerSlots[i].SetReady(readys[i]);
            }
        }
    }

    private void StartMatch()
    {
        atLobby = false;
        _matchOver = false;
        playerSlotsInGame.Clear();
        playerScores.Value.Clear();

        foreach (PlayerSlot slot in _playerSlots)
        {
            if (slot.enabled && slot.GetInput() != null)
            {
                playerSlotsInGame.Add(slot);
                playerScores.Value.Add(0);
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
        }
        matchStart.Raise();
        Cursor.lockState = CursorLockMode.Locked;
        _firstRound = true;
        StopCoroutine("CheckReadyAndNewJoystick");
        StartCoroutine("StartRound");
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(0.5f);
        SetStage();
        _endingRound = false;
        roundSetted.Raise();
        yield return new WaitForSeconds(3.5f);
        foreach (PlayerSlot slot in playerSlotsInGame)
        {
            slot.FreePlayer();
        }
        roundStart.Raise();
    }

    public void Death()
    {
        if (!enabled)
        {
            return;
        }
        SetScores();
        CheckIsMatchOver();
    }

    private void SetScores()
    {
        //if (lastIdScored.Value == -2) return;
        //for (int i = 0; i < playerSlotsInGame.Count; i++)
        //{
        //    if (playerSlotsInGame[i].GetComponent<IInput>().id == lastIdScored.Value)
        //    {
        //        playerScores.Value[i]++;
        //        lastIdScored.Value = -2;
        //    }
        //}
    }

    private void CheckIsMatchOver()
    {
        foreach (int score in playerScores.Value)
        {
            if (score >= scoreToWin)
            {
                _matchOver = true;
                StartCoroutine("FinishMatch");
            }
        }

        if (!_matchOver)
        {
            CheckIsRoundOver();
        }
    }

    private void CheckIsRoundOver()
    {
        int playersAlive = 0;
        foreach (PlayerSlot slot in playerSlotsInGame)
        {
            if (slot.IsPlayerAlive())
            {
                playersAlive++;
            }
        }
        if (playersAlive <= 1 && !_endingRound)
        {
            _endingRound = true;
            StartCoroutine("FinishRound");
        }
    }

    public IEnumerator FinishRound()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < players.Value.Count; i++)
        {
            if (players.Value[i].activeSelf)
            {
                playerScores.Value[i]++;
            }
        }
        CheckIsMatchOver();
        if (_matchOver)
        {
            StopCoroutine("FinishRound");
        }
        else
        {
            roundEnd.Raise();
            yield return new WaitForSeconds(0.3f);
            StartCoroutine("StartRound");
        }
    }

    public IEnumerator FinishMatch()
    {
        matchEnd.Raise();
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(0.8f);
        atLobby = true;
        Destroy(currentStage);
        currentStage = Instantiate(stages.Value[0]);
        foreach (PlayerSlot slot in _playerSlots)
        {
            slot.gameObject.SetActive(true);
            slot.SetPlayerPosition(slot.transform.position);
            slot.SetReady(false);
        }
        StartCoroutine("CheckReadyAndNewJoystick");
    }

    private void SetStage()
    {
        Destroy(currentStage);
        if (_firstRound)
        {
            currentStage = Instantiate(stages.Value[1]);
            _firstRound = false;
        }
        else
        {
            currentStage = Instantiate(stages.Value[Random.Range(2, stages.Value.Count)]);
        }
        int i = 0;
        foreach (PlayerSlot slot in playerSlotsInGame)
        {
            slot.SetPlayerPosition(currentStage.transform.GetChild(0).GetChild(i).position);
            i++;
        }
    }

    public void ToMainMenu()
    {
        StartCoroutine("ResetController");
    }

    private IEnumerator ResetController()
    {
        yield return new WaitForSeconds(0.8f);
        goMainMenu.Raise();
        Cursor.lockState = CursorLockMode.None;
        StopAllCoroutines();
        SetStartVariables();
        atLobby = false;
        Destroy(currentStage);
        currentStage = Instantiate(stages.Value[0]);
        foreach (PlayerSlot slot in _playerSlots)
        {
            Destroy(slot.gameObject);
        }
        playerSlotsInGame.Clear();
        _playerSlots.Clear();
        enabled = false;
    }

    private void OnDestroy()
    {
        foreach (PlayerSlot slot in _playerSlots)
        {
            slot.leave -= PlayerLeave;
        }
    }
}
