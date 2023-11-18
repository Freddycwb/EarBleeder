using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventManager : MonoBehaviour, IGameEventListener
{
    [Tooltip("Events to register with.")]
    public GameEvent[] Events;
    public GameObject[] sounds;
    //public EventReference[] FMODEvents;

    //private void Awake()
    //{
    //    if (Events.Length > FMODEvents.Length)
    //    {
    //        Debug.LogError($"ERROR: The size of the Events array is larger than the size of the FMODEvents array in object {gameObject.name}. You probably forgot to add an FMODEvent.");
    //    }
    //}

    private void OnEnable()
    {
        foreach (GameEvent e in Events)
        {
            e.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        foreach (GameEvent e in Events)
        {
            e.UnregisterListener(this);
        }
    }

    public void OnEventRaised(params object[] parameters)
    {
        int eventIndex = Array.IndexOf(Events, parameters[0] as GameEvent);

        if (eventIndex > sounds.Length)
        {
            Debug.LogError($"ERROR: The size of the Events array is larger than the size of the FMODEvents array in object {gameObject.name}. You probably forgot to add an FMODEvent.");
            return;
        }
        if (eventIndex < 0) // De algum jeito, o evento que foi escutado não está na array de eventos
        {
            Debug.LogError($"ERROR: Object {gameObject.name} listened to a GameEvent that is not in the array of events. Maybe the array was altered during execution?");
            return;
        }

        Instantiate(sounds[eventIndex]);
        //RuntimeManager.PlayOneShot(FMODEvents[eventIndex]);
        
    }
}
