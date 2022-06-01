using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundManagerLocal : MonoBehaviour
{
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        SoundManagerGlobal.OnSoundOnOff += SetSoundOnOff;
        SoundManagerGlobal.OnSoundValueChanged += SetSoundValue;
        MenuButtons.OnCanMove += GameIsPlayed;
    }

    protected void SetSoundOnOff(bool flag)
    {
        audioSource.mute = !flag;
    }

    protected void SetSoundValue(float soundValue)
    {
        audioSource.volume = soundValue;
    }

    protected virtual void GameIsPlayed(bool flag)
    {
        if (!flag)
        {
            audioSource.Pause();
        }
        else
            audioSource.Play();
    }

    private void OnDestroy()
    {
        SoundManagerGlobal.OnSoundOnOff -= SetSoundOnOff;
        SoundManagerGlobal.OnSoundValueChanged -= SetSoundValue;
        MenuButtons.OnCanMove -= GameIsPlayed;
    }
}
