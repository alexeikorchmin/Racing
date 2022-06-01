using UnityEngine;
using System;

public class DistanceFromCamera : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float distance = 100f;

    public static Action<bool> OnCloseDistance;

    void Update()
    {
        CheckTheDistance();
    }

    private void CheckTheDistance()
    {
        if (Vector3.Distance(transform.position, cameraObject.transform.position) <= distance)
        {
            OnCloseDistance?.Invoke(true);
        }
        else
            OnCloseDistance?.Invoke(false);
    }    
}
