using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SteerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static Action<bool, float> OnSteerButtonPressed;

    [SerializeField] private float steerTurnValue;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSteerButtonPressed?.Invoke(true, steerTurnValue);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnSteerButtonPressed?.Invoke(false, steerTurnValue);
    }
}