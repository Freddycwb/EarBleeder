using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeFocus : MonoBehaviour
{
    [SerializeField] private VolumeProfile _volume;
    [SerializeField] private DepthOfField _depthOfField;
    private bool _defocus;

    [SerializeField] private int defocusFocalLength;
    [SerializeField] private float transitionSpeed;

    public void SetDefocus(bool value)
    {
        _defocus = value;
    }

    private void Start()
    {
        for (int i = 0; i < _volume.components.Count; ++i)
        {
            if (_volume.components[i].GetType() == typeof(DepthOfField))
            {
                _depthOfField = (DepthOfField)_volume.components[i];
                break;
            }
        }
    }

    void Update()
    {
        FocusTransition();
    }

    private void FocusTransition()
    {
        if (_defocus && _depthOfField.focalLength.value < defocusFocalLength)
        {
            _depthOfField.focalLength.value += Time.unscaledDeltaTime * transitionSpeed;
            if (_depthOfField.focalLength.value > defocusFocalLength)
            {
                _depthOfField.focalLength.value = defocusFocalLength;
            }
        }
        else if (!_defocus && _depthOfField.focalLength.value > 0)
        {
            _depthOfField.focalLength.value -= Time.unscaledDeltaTime * transitionSpeed;
            if (_depthOfField.focalLength.value < 0)
            {
                _depthOfField.focalLength.value = 0;
            }
        }
    }
}
