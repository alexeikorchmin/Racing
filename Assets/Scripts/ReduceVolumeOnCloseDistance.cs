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

        DistanceFromCamera.OnCloseDistance += ReduceVolume;
        SoundManagerGlobal.OnSoundValueChanged += SoundValue;
    }

    private void SoundValue(float soundValue)
    {
        NonReducedVolume = soundValue;
        reducedVolume = NonReducedVolume / 5;
    }

    private void ReduceVolume(bool flag)
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
        DistanceFromCamera.OnCloseDistance -= ReduceVolume;
    }
}
