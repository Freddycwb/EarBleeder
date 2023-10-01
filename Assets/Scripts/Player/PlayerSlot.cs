using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using System;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    [SerializeField] private GameObjectListVariable players;
    [SerializeField] private int playerSlotID;
    private bool _ready;
    [SerializeField] private TextMeshPro tmp;
    [SerializeField] private Image circleImage;
    private PlayerInput _input;
    [SerializeField] private GameObject player;
    private GameObject _currentPlayer;
    [SerializeField] private GameObjectListVariable skinsUnselected;
    private int _currentSkin;
    private bool _changingSkin;

    private float pressTime;
    private bool isPressing;
    private float timeToStart = 1.5f;
    
    public Action<int> leave;

    [SerializeField] private ParticleSystem smoke;


    private void OnEnable()
    {
        tmp.text = tmp.text = Localization.Localize("_pressToJoin");
    }

    public int GetInputID()
    {
        int id = 0;
        if (_input != null)
        {
            id = _input.id;
        }
        return id;
    }

    public int GetPlayerSlotID()
    {
        return playerSlotID;
    }

    public void SetInput(int id, string debug)
    {
        enabled = true;
        smoke.Play();
        _currentPlayer = Instantiate(player, transform.position, transform.rotation);
        _currentPlayer.transform.eulerAngles = new Vector3(0,180,0);
        _currentPlayer.GetComponent<PlayerInput>().SetCanControl(false);
        _currentPlayer.GetComponent<PlayerInput>().SetID(id);
        _currentSkin = playerSlotID;
        _currentPlayer.GetComponentInChildren<PlayerAnimator>().SetBody(skinsUnselected.Value[_currentSkin]);
        players.Value.Add(_currentPlayer);
        tmp.text = Localization.Localize("_pressToReady");
        StartCoroutine("SetInputAfterTime", id);
    }

    private IEnumerator SetInputAfterTime(int id)
    {
        if (GetComponent<PlayerInput>() != null)
        {
            Destroy(GetComponent<PlayerInput>());
        }
        yield return new WaitForEndOfFrame();
        gameObject.AddComponent<PlayerInput>().SetID(id);
        _input = GetComponent<PlayerInput>();
    }

    public IInput GetInput()
    {
        return _input;
    }

    public void SetID(int value)
    {
        playerSlotID = value;
    }

    public void SetReady(bool ready)
    {
        _ready = ready;
    }

    public bool GetReady()
    {
        return _ready;
    }

    public bool IsPlayerAlive()
    {
        return _currentPlayer != null && _currentPlayer.activeSelf;
    }

    public void SetSkin(int skin)
    {
        _currentSkin = skin;
        _currentPlayer.GetComponentInChildren<PlayerAnimator>().SetBody(skinsUnselected.Value[_currentSkin]);
    }

    public int GetSkin()
    {
        return _currentSkin;
    }

    private void Update()
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.transform.position = transform.position;
        }
        SelectSkin();
        Backs();
    }


    private void SelectSkin()
    {
        if (_input != null && !_ready)
        {
            if (_input.aButtonDown)
            {
                isPressing = true;
            }

            if (_input.aButtonUp)
            {
                isPressing = false;
                pressTime = 0f;
                circleImage.fillAmount = 0;
            }

            if (isPressing)
            {
                pressTime += Time.deltaTime;
                float fillPercentage = Mathf.Clamp01(pressTime / timeToStart);
                circleImage.fillAmount = fillPercentage;

                if (pressTime >= timeToStart)
                {
                    _ready = true;
                    tmp.text = Localization.Localize("_ready");
                    pressTime = 0f;
                    isPressing = false;
                }
            }
            ChangeSkin();
        }
    }

    private void ChangeSkin()
    {
        if ((_input.direction.x >= 0.5f || _input.direction.x < -0.5f) && !_changingSkin)
        {
            if (_input.direction.x < 0)
            {
                _currentSkin = _currentSkin == 0 ? skinsUnselected.Value.Count - 1 : _currentSkin - 1;
            }
            else
            {
                _currentSkin = _currentSkin == skinsUnselected.Value.Count - 1 ? 0 : _currentSkin + 1;
            }
            _currentPlayer.GetComponentInChildren<PlayerAnimator>().SetBody(skinsUnselected.Value[_currentSkin]);
            _changingSkin = true;
        }
        else if (_input.direction.x < 0.5f && _input.direction.x > -0.5f) 
        {
            _changingSkin = false;
        }
    }

    private void Backs()
    {
        if (_input != null && _ready && _input.bButtonUp)
        {
            _ready = false;
            tmp.text = Localization.Localize("_pressToReady");
        }
        if (_input != null && !_ready && _input.bButtonDown)
        {
            if (leave != null)
            {
                leave.Invoke(_input.id);
            }
        }
    }

    public void Leave()
    {
        smoke.Play();
        Destroy(_input);
        _input = null;
        players.Value.Remove(_currentPlayer);
        Destroy(_currentPlayer);
        tmp = GetComponentInChildren<TextMeshPro>();
        tmp.text = Localization.Localize("_pressToJoin");
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        if (_currentPlayer == null) return;
        _currentPlayer.SetActive(true);
        _currentPlayer.GetComponent<PlayerInput>().SetCanControl(false);
        _currentPlayer.GetComponent<Player>().ResetState();
        _currentPlayer.transform.position = pos;
        _currentPlayer.transform.eulerAngles = new Vector3(0, -180, 0);
    }

    public void FreePlayer()
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.GetComponent<PlayerInput>().SetCanControl(true);
        }
        gameObject.SetActive(false);
    }

    public void Disconnected()
    {
        if (_input != null)
        {
            smoke.Play();
            Destroy(_input);
            _input = null;
            players.Value.Remove(_currentPlayer);
            Destroy(_currentPlayer);
        }
        enabled = false;
    }

    private void OnDisable()
    {
        tmp.text = tmp.text = Localization.Localize("_connect");
    }
}
