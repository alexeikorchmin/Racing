using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SteerRightController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static Action<bool> OnRightButtonPressed;

    private bool isButtonPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        OnRightButtonPressed?.Invoke(isButtonPressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        OnRightButtonPressed?.Invoke(isButtonPressed);
    }
}