using UnityEngine;

public class ReduceVolumeOnCloseDistance : MonoBehaviour
{
    private AudioSource audioSource;
    private float NonReducedVolume;
    private float reducedVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        NonReducedVolume = audioSource.volume;
        reducedVolume = NonReducedVolume / 5;

        DistanceFromCamera.OnCloseDistance += OnCloseDistanceHandler;
        SoundManagerGlobal.OnSoundValueChanged += OnSoundValueChangedHandler;
    }

    private void OnSoundValueChangedHandler(float soundValue)
    {
        NonReducedVolume = soundValue;
        reducedVolume = NonReducedVolume / 5;
    }

    private void OnCloseDistanceHandler(bool flag)
    {
        if (flag)
        {
            audioSource.volume = reducedVolume;
        }
        else
        {
            audioSource.volume = NonReducedVolume;
        }
    }

    private void OnDestroy()
    {
        DistanceFromCamera.OnCloseDistance -= OnCloseDistanceHandler;
        SoundManagerGlobal.OnSoundValueChanged -= OnSoundValueChangedHandler;
    }
}