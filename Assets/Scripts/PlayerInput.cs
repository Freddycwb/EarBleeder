using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInput : MonoBehaviour, IInput
{
    private bool canControl = true;

    public int _id;

    public void SetID(int value)
    {
        _id = value;
        //test
    }

    public Vector2 direction
    {
        get
        {
            Vector2 gamepadMove = Vector2.zero;
            if (Gamepad.all[_id] != null)
            {
                StickControl stick = Gamepad.all[_id].leftStick;
                gamepadMove = new Vector2(stick.right.value - stick.left.value, stick.up.value - stick.down.value);
                if (gamepadMove.magnitude < 0.5f) 
                {
                    gamepadMove = Vector2.zero;
                }
            }
            Vector2 move = gamepadMove;

            if (canControl)
            {
                return move;
            }
            else
            {
                return Vector2.zero;
            }
        }
    }

   
    public void SetCanControl(bool state)
    {
        canControl = state;
    }
}
