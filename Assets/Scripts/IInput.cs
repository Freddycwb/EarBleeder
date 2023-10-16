using UnityEngine;

public interface IInput
{
    int id { get; }

    Vector2 direction { get; }

    bool startButtonDown { get; }
    bool startButton { get; }
    bool startButtonUp { get; }

    bool aButtonDown { get; }
    bool aButton { get; }
    bool aButtonUp { get; }

    bool bButtonDown { get; }
    bool bButton { get; }
    bool bButtonUp { get; }

    bool fireButtonDown { get; }
    bool fireButton { get; }
    bool fireButtonUp { get; }

    bool escapeButtonDown { get; }
}