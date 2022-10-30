using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundManagerLocal : MonoBehaviour
{
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        SoundManagerGlobal.OnSoundOnOff += OnSoundOnOffHandler;
        SoundManagerGlobal.OnSoundValueChanged += OnSoundValueChangedHandler;
        GameManager.OnCanMove += OnCanMoveHandler;
    }

    protected void OnSoundOnOffHandler(bool flag)
    {
        audioSource.mute = !flag;
    }

    protected void OnSoundValueChangedHandler(float soundValue)
    {
        audioSource.volume = soundValue;
    }

    protected virtual void OnCanMoveHandler(bool flag)
    {
        if (!flag)
            audioSource.Pause();
        else
            audioSource.Play();
    }

    private void OnDestroy()
    {
        SoundManagerGlobal.OnSoundOnOff -= OnSoundOnOffHandler;
        SoundManagerGlobal.OnSoundValueChanged -= OnSoundValueChangedHandler;
        GameManager.OnCanMove -= OnCanMoveHandler;
    }
}