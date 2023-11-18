using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] private bool isMusic;

    [SerializeField] private FloatVariable masterVolume;
    [SerializeField] private FloatVariable musicVolume;
    [SerializeField] private FloatVariable sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        source.volume = isMusic ? musicVolume.Value * masterVolume.Value : sfxVolume.Value * masterVolume.Value;
    }
}
