using UnityEngine;

public class CarTiresSoundHandler : MonoBehaviour
{
    private float timer;
    private bool isButtonPressed;

    private void Awake()
    {
        SteerController.OnSteerButtonPressed += OnSteerButtonPressedHandler;
    }

    private void OnDestroy()
    {
        SteerController.OnSteerButtonPressed -= OnSteerButtonPressedHandler;
    }

    private void Update()
    {
        if (isButtonPressed == false) { return; }

        timer += Time.deltaTime;
        if (timer > 1f)
            SoundManagerGlobal.Instance.PlayTiresSound();
    }

    private void OnSteerButtonPressedHandler(bool isPressed, float steerValue)
    {
        isButtonPressed = isPressed;

        if (!isPressed)
            timer = 0f;
    }
}