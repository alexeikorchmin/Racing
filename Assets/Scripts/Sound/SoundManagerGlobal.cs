using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerGlobal : MonoBehaviour
{
    public static SoundManagerGlobal Instance;

    [SerializeField] private Slider soundSlider;
    [SerializeField] private Toggle soundToggle;

    [SerializeField] private List<AudioSource> audioSources;
    [SerializeField] private AudioSource carMotorAudioSource;
    [SerializeField] private AudioSource carCrashAudioSource;
    [SerializeField] private AudioSource tiresAudioSource;
    [SerializeField] private AudioSource planeAudioSource;

    private bool canMove;

    public void PlayTiresSound()
    {
        if (!canMove) { return; }

        tiresAudioSource.PlayOneShot(tiresAudioSource.clip);
    }

    public void PlayCarCrashSound()
    {
        carCrashAudioSource.PlayOneShot(carCrashAudioSource.clip);
    }

    public void ReduceVolumeOnCloseDistance(List<AudioSource> audioSources, bool isClose)
    {
        foreach (var source in audioSources)
        {
            if (isClose)
                source.volume = soundSlider.value / 5;
            else
                source.volume = soundSlider.value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        soundSlider.onValueChanged.AddListener(SoundSliderValue);
        soundToggle.onValueChanged.AddListener(SoundToggleValue);

        GameManager.OnCanMove += OnCanMoveHandler;
    }

    private void OnDestroy()
    {
        GameManager.OnCanMove -= OnCanMoveHandler;
    }

    private void SoundSliderValue(float soundSliderValue)
    {
        foreach (var source in audioSources)
        {
            source.volume = soundSliderValue;
        }
    }

    private void SoundToggleValue(bool flag)
    {
        foreach (var source in audioSources)
        {
            source.mute = flag;
        }
    }

    private void OnCanMoveHandler(bool canMove)
    {
        this.canMove = canMove;
        
        foreach (var source in audioSources)
        {
            if (canMove)
            {
                if (source != carCrashAudioSource && source != tiresAudioSource)
                source.Play();
            }
            else
            {
                source.Stop();
            }
        }
    }
}