using System;
using UnityEngine;

public class DistanceFromCamera : MonoBehaviour
{
    public static Action<bool> OnCloseDistance;

    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float distance = 100f;

    private void Update()
    {
        CheckTheDistance();
    }

    private void CheckTheDistance()
    {
        if (Vector3.Distance(transform.position, cameraObject.transform.position) <= distance)
            OnCloseDistance?.Invoke(true);
        else
            OnCloseDistance?.Invoke(false);
    } 
}