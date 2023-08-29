using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInput : MonoBehaviour, IInput
{
    private bool canControl = true;

    [SerializeField] private int _id;

    public void SetID(int value)
    {
        _id = value;
    }

    public int id
    {
        get { return _id; }
    }

    public Vector2 direction
    {
        get
        {
            Vector2 move = Vector2.zero;
            if (_id != -1)
            {
                Vector2 gamepadMove = Vector2.zero;
                if (_id <= Gamepad.all.Count - 1)
                {
                    StickControl stick = Gamepad.all[_id].leftStick;
                    gamepadMove = new Vector2(stick.right.value - stick.left.value, stick.up.value - stick.down.value);
                    if (gamepadMove.magnitude < 0.5f)
                    {
                        gamepadMove = Vector2.zero;
                    }
                }
                move = gamepadMove;
            }
            else
            {
                int x = 0;

                x += Input.GetKey(KeyCode.A) ? -1 : 0;
                x += Input.GetKey(KeyCode.D) ? 1 : 0;

                int y = 0;
                y += Input.GetKey(KeyCode.W) ? 1 : 0;
                y += Input.GetKey(KeyCode.S) ? -1 : 0;

                move = new Vector2(x, y);
            }
            

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

    public bool aButtonDown
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyDown(KeyCode.J);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonSouth.wasPressedThisFrame;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool aButton
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyDown(KeyCode.J);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonSouth.isPressed;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool aButtonUp
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyUp(KeyCode.J);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonSouth.wasReleasedThisFrame;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool bButtonDown
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyDown(KeyCode.Q);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonEast.wasPressedThisFrame;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool bButton
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyDown(KeyCode.Q);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonEast.isPressed;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool bButtonUp
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyUp(KeyCode.Q);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonEast.wasReleasedThisFrame;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool fireButtonDown
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyDown(KeyCode.L);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonWest.wasPressedThisFrame;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool fireButton
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKey(KeyCode.L);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonWest.isPressed;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool fireButtonUp
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyUp(KeyCode.L);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].buttonWest.wasReleasedThisFrame;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public bool escapeButtonDown
    {
        get
        {
            bool click = false;
            if (_id == -1)
            {
                click = Input.GetKeyDown(KeyCode.Escape);
            }
            else
            {
                if (_id <= Gamepad.all.Count - 1)
                {
                    click = Gamepad.all[_id].leftShoulder.wasPressedThisFrame;
                }
            }

            if (canControl)
            {
                return click;
            }
            else
            {
                return false;
            }
        }
    }

    public void SetCanControl(bool state)
    {
        canControl = state;
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
