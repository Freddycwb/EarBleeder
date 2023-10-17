using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class MenuPlayerInput : MonoBehaviour, IInput
{
    private bool canControl = true;

    [SerializeField] private int _id = -2;

    public int id
    {
        get { return _id; }
    }

    public Vector2 direction
    {
        get
        {
            return Vector2.zero;
        }
    }

    public bool startButtonDown
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].startButton.wasPressedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyDown(KeyCode.Escape) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool startButton
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].startButton.isPressed)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKey(KeyCode.Escape) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool startButtonUp
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].startButton.wasReleasedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyUp(KeyCode.Escape) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }


    public bool aButtonDown
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].buttonSouth.wasPressedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Return) || gamepads;
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
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].buttonSouth.isPressed)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Return) || gamepads;
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
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].buttonSouth.wasReleasedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.Return) || gamepads;
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
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].buttonEast.wasPressedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyDown(KeyCode.K) || gamepads;
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
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].buttonEast.isPressed)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKey(KeyCode.K) || gamepads;
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
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].buttonEast.wasReleasedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyUp(KeyCode.K) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }


    public bool dPadDownButtonDown
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.down.wasPressedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool dPadDownButton
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.down.isPressed)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool dPadDownButtonUp
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.down.wasReleasedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }



    public bool dPadUpButtonDown
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.up.wasPressedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool dPadUpButton
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.up.isPressed)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool dPadUpButtonUp
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.up.wasReleasedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }



    public bool dPadLeftButtonDown
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.left.wasPressedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool dPadLeftButton
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.left.isPressed)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool dPadLeftButtonUp
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.left.wasReleasedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }



    public bool dPadRightButtonDown
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.right.wasPressedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool dPadRightButton
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.right.isPressed)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || gamepads;
            }
            else
            {
                return false;
            }
        }
    }

    public bool dPadRightButtonUp
    {
        get
        {
            bool gamepads = false;
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                if (Gamepad.all[i].dpad.right.wasReleasedThisFrame)
                {
                    gamepads = true;
                }
            }
            if (canControl)
            {
                return Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || gamepads;
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
            return false;
        }
    }

    public bool fireButton
    {
        get
        {
            return false;
        }
    }

    public bool fireButtonUp
    {
        get
        {
            return false;
        }
    }

    public bool escapeButtonDown
    {
        get
        {
            return false;
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
