using System;
using System.Collections;
using UnityEngine;

public class CarMovementController : MonoBehaviour
{
    public static Action<float> OnSpeedValue;
    public static Action<bool> OnSpeedIncreased;
    public static Action OnObstacleBumped;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float steerTurnSpeed = 50f;
    [SerializeField] private float steerDirectionValue = 1f;

    private bool isSteerButtonPressed;
    private bool canMove = false;

    private void Awake()
    {
        SteerController.OnSteerButtonPressed += OnSteerButtonPressedHandler;
        GameManager.OnCanMove += CanMoveMethod;
    }

    private void Update()
    {
        OnSpeedValue?.Invoke(speed);

        if (canMove)
        {
            MoveCar();
        }
    }

    private void MoveCar()
    {
        if (speed >= maxSpeed)
        {
            acceleration = 0f;
            speed = maxSpeed;
        }

        speed += acceleration * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (isSteerButtonPressed)
        {
            transform.Rotate(0f, steerDirectionValue * steerTurnSpeed * Time.deltaTime, 0f);
        }
    }

    public void OnSteerButtonPressedHandler(bool isButtonPressed, float steerDirection)
    {
        isSteerButtonPressed = isButtonPressed;
        steerDirectionValue = steerDirection;
    }

    private void CanMoveMethod(bool flag)
    {
        canMove = flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Obstacle":
                OnObstacleBumped?.Invoke();
                break;
            case "SpeedIncrease":
                OnSpeedIncreased?.Invoke(true);
                StartCoroutine(SetCarMovementValues(5f, 2f, 10f));
                break;
            case "SpeedReduce":
                OnSpeedIncreased?.Invoke(false);
                StartCoroutine(SetCarMovementValues(-2.5f, 0f, -10f));
                break;
        }
    }

    private IEnumerator SetCarMovementValues(float addMaxSpeed, float Acceleration, float addSteerTurnSpeed)
    {
        yield return new WaitForSeconds(1f);
        maxSpeed += addMaxSpeed;
        acceleration = Acceleration;
        steerTurnSpeed += addSteerTurnSpeed;
    }

    private void OnDestroy()
    {
        SteerController.OnSteerButtonPressed -= OnSteerButtonPressedHandler;
        GameManager.OnCanMove -= CanMoveMethod;
    }
}