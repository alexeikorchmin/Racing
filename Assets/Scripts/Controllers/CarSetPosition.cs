using UnityEngine;

public class CarSetPosition : MonoBehaviour
{
    private Vector3 carStartPosition;
    private Quaternion carStartRotation;

    private void Awake()
    {
        AdManager.OnAdFinished += OnAdFinishedHandler;
        Init();
    }

    private void OnDestroy()
    {
        AdManager.OnAdFinished -= OnAdFinishedHandler;
    }

    private void Init()
    {
        carStartPosition = transform.position;
        carStartRotation = transform.rotation;
    }

    private void OnAdFinishedHandler()
    {
        transform.position = carStartPosition;
        transform.rotation = carStartRotation;
    }
}