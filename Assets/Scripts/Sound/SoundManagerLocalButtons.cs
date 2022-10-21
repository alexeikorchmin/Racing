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
        SteerLeftController.OnLeftButtonPressed += OnSteerButtonPressedHandler;
        SteerRightController.OnRightButtonPressed += OnSteerButtonPressedHandler;
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

    private void OnSteerButtonPressedHandler(bool flag)
    {
        isButtonPressed = flag;

        if (!flag)
        {
            timer = 0f;
        }
    }

    private void OnDestroy()
    {
        SteerLeftController.OnLeftButtonPressed -= OnSteerButtonPressedHandler;
        SteerRightController.OnRightButtonPressed -= OnSteerButtonPressedHandler;
    }
}