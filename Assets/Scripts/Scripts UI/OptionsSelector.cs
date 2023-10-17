using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsSelector : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInput _input;

    [SerializeField] private List<Button> btns = new List<Button>();
    private List<Animator> _btnAnimators = new List<Animator>();
    private int _currentBtnSelected;

    public void SetInput()
    {
        _input = input.GetComponent<IInput>();
    }

    private void Start()
    {
        foreach (var button in btns)
        {
            _btnAnimators.Add(button.GetComponent<Animator>());
        }
        SelectBtn(0);
        _btnAnimators[0].Play("ButtonOverlap");
    }

    private void Update()
    {
        if (_input == null) { return; }
        MoveSelection();
        ClickBtn();
    }

    private void MoveSelection()
    {
        if (_input.dPadDownButtonDown)
        {
            SelectBtn(_currentBtnSelected + 1);
        }
        else if (_input.dPadUpButtonDown)
        {
            SelectBtn(_currentBtnSelected - 1);
        }
    }

    private void ClickBtn()
    {
        if (_input.aButtonDown || Input.GetKeyDown(KeyCode.Return))
        {
            btns[_currentBtnSelected].onClick.Invoke();
            _btnAnimators[_currentBtnSelected].Play("ButtonClick", -1, 0f);
        }
    }

    public void SelectBtn(int id)
    {
        if (id == _currentBtnSelected) { return; }
        _btnAnimators[_currentBtnSelected].Play("ButtonNoOverlap");
        if (id >= btns.Count)
        {
            _currentBtnSelected = id - btns.Count;
        }
        else if (id < 0)
        {
            _currentBtnSelected = id + btns.Count;
        }
        else
        {
            _currentBtnSelected = id;
        }
        _btnAnimators[_currentBtnSelected].Play("ButtonOverlap");
    }
}
