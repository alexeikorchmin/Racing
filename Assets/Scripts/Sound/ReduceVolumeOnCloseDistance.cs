using UnityEngine;

public class ReduceVolumeOnCloseDistance : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceToReduce;

    private float NonReducedVolume;
    private float reducedVolume;

    private void Awake()
    {
        DistanceFromCamera.OnCloseDistance += OnCloseDistanceHandler;
        SoundManagerGlobal.OnSoundValueChanged += OnSoundValueChangedHandler;
        Init();
    }

    private void Init()
    {
        NonReducedVolume = audioSourceToReduce.volume;
        reducedVolume = NonReducedVolume / 5;
    }

    private void OnSoundValueChangedHandler(float soundValue)
    {
        NonReducedVolume = soundValue;
        reducedVolume = NonReducedVolume / 5;
    }

    private void OnCloseDistanceHandler(bool flag)
    {
        if (flag)
            audioSourceToReduce.volume = reducedVolume;
        else
            audioSourceToReduce.volume = NonReducedVolume;
    }

    private void OnDestroy()
    {
        DistanceFromCamera.OnCloseDistance -= OnCloseDistanceHandler;
        SoundManagerGlobal.OnSoundValueChanged -= OnSoundValueChangedHandler;
    }
}