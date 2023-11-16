using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsInputSetting : MonoBehaviour
{
    [SerializeField] private OptionsSelector buttons;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pause;
    [SerializeField] private InvokeAfter optionsMove;
    [SerializeField] private InvokeAfter mainMenuMove;
    [SerializeField] private InvokeAfter pauseMove;

    private bool cameFromMainMenu;

    public void OptionsCall(bool callByMainMenu)
    {
        if (callByMainMenu)
        {
            buttons.SetInput(mainMenu);
        }
        else
        {
            buttons.SetInput(pause);
        }
        cameFromMainMenu = callByMainMenu;
        In();
    }

    private void In()
    {
        optionsMove.CallAction();
        if (cameFromMainMenu)
        {
            mainMenuMove.CallSubAction();
        }
        else
        {
            pauseMove.CallSubAction();
        }
    }

    public void Back()
    {
        optionsMove.CallSubAction();
        if (cameFromMainMenu)
        {
            mainMenuMove.CallAction();
        }
        else
        {
            pauseMove.CallAction();
        }
    }
}
