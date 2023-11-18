using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeSetter : MonoBehaviour
{
    [SerializeField] private FloatVariable masterVolume;
    [SerializeField] private FloatVariable musicVolume;
    [SerializeField] private FloatVariable sfxVolume;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    void Update()
    {
        masterVolume.Value = masterSlider.value;
        musicVolume.Value = musicSlider.value;
        sfxVolume.Value = sfxSlider.value;
    }
}
