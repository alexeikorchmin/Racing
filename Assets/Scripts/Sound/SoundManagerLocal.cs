using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundManagerLocal : MonoBehaviour
{
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        SoundManagerGlobal.OnSoundValueChanged += OnSoundValueChangedHandler;
        SoundManagerGlobal.OnSoundOnOff += OnSoundOnOffHandler;
        GameManager.OnCanMove += OnCanMoveHandler;
    }

    protected virtual void OnSoundValueChangedHandler(float soundValue)
    {
        audioSource.volume = soundValue;
    }

    protected virtual void OnCanMoveHandler(bool canMove)
    {
        if (!canMove)
            audioSource.Pause();
        else
            audioSource.Play();
    }

    protected void OnSoundOnOffHandler(bool flag)
    {
        audioSource.mute = !flag;
    }

    protected virtual void OnDestroy()
    {
        SoundManagerGlobal.OnSoundValueChanged -= OnSoundValueChangedHandler;
        SoundManagerGlobal.OnSoundOnOff -= OnSoundOnOffHandler;
        GameManager.OnCanMove -= OnCanMoveHandler;
    }
}