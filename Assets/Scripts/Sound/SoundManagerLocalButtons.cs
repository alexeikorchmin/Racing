using UnityEngine;

public class SoundManagerLocalButtons : SoundManagerLocal
{
    [SerializeField] private AudioClip audioClip;

    private float timer = 0f;
    private bool isButtonPressed = false;

    protected override void OnCanMoveHandler(bool flag) { }

    protected override void Awake()
    {
        base.Awake();
        SteerController.OnSteerButtonPressed += OnSteerButtonPressedHandler;
    }

    private void Update()
    {
        if (isButtonPressed)
        {
            timer += Time.deltaTime;

            if (timer > 1f)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }

    private void OnSteerButtonPressedHandler(bool flag, float steerValue)
    {
        isButtonPressed = flag;

        if (!flag)
        {
            timer = 0f;
        }
    }

    private void OnDestroy()
    {
        SteerController.OnSteerButtonPressed -= OnSteerButtonPressedHandler;
    }
}