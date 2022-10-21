using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerGlobal : MonoBehaviour
{
    public static Action<float> OnSoundValueChanged;
    public static Action<bool> OnSoundOnOff;

    [SerializeField] private Slider soundSlider;
    [SerializeField] private Toggle soundToggle;

    private void Awake()
    {
        soundSlider.onValueChanged.AddListener(SoundSliderValue);
        soundToggle.onValueChanged.AddListener(SoundToggleValue);
    }

    private void SoundSliderValue(float soundSliderValue)
    {
        OnSoundValueChanged?.Invoke(soundSlider.value);
    }

    private void SoundToggleValue(bool flag)
    {
        OnSoundOnOff?.Invoke(soundToggle.isOn);
    }
}