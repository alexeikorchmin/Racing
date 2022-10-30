using UnityEngine;

public class SoundManagerLocalButtons : SoundManagerLocal
{
    private float soundReduceMultiplier = 5f;
    private float timer = 0f;
    private bool isButtonPressed = false;

    protected override void Awake()
    {
        base.Awake();
        SteerController.OnSteerButtonPressed += OnSteerButtonPressedHandler;
        audioSource.volume = 0.2f;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SteerController.OnSteerButtonPressed -= OnSteerButtonPressedHandler;
    }

    protected override void OnSoundValueChangedHandler(float soundValue)
    {
        audioSource.volume = soundValue / soundReduceMultiplier;
    }

    protected override void OnCanMoveHandler(bool canMove)
    {
        if (audioSource == null) { return; }

        if (!canMove)
            audioSource.mute = true;
        else
            audioSource.mute = false;
    }

    private void Update()
    {
        if (isButtonPressed)
        {
            timer += Time.deltaTime;

            if (timer > 1f)
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    private void OnSteerButtonPressedHandler(bool flag, float steerValue)
    {
        isButtonPressed = flag;

        if (!flag)
            timer = 0f;
    }
}