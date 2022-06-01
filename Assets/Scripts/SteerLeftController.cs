using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SteerLeftController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static Action<bool> OnLeftButtonPressed;

    private bool isButtonPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        OnLeftButtonPressed?.Invoke(isButtonPressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        OnLeftButtonPressed?.Invoke(isButtonPressed);
    }
}
