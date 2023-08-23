using UnityEngine;

public interface IInput
{
    Vector2 direction { get; }

    bool aButtonDown { get; }
    bool aButton { get; }
    bool aButtonUp { get; }

    bool bButtonDown { get; }
    bool bButton { get; }
    bool bButtonUp { get; }
}