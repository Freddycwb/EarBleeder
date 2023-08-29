using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMusic : MonoBehaviour
{
    private IInput _input;
    private AudioSource _source;

    [SerializeField] private float volumeSpeed;

    void Start()
    {
        _input = GetComponent<IInput>();
        _source = GetComponent<AudioSource>();
    }

    void Update()
    {
        SetVolume();
    }

    private void SetVolume()
    {
        if (_input.fireButton)
        {
            _source.volume = _source.volume < 1 ? _source.volume + Time.deltaTime * volumeSpeed : 1;
        }
        else
        {
            _source.volume = _source.volume > 0 ? _source.volume - Time.deltaTime * volumeSpeed : 0;
        }
    }
}
