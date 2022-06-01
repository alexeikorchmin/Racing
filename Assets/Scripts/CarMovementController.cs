using UnityEngine;
using System;
using System.Collections;

public class CarMovementController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float steerTurnSpeed = 50f;
    [SerializeField] private float steerDirectionValue = 1f;

    private bool isLeftButtonPressed;
    private bool isRightButtonPressed;
    private bool canMove = false;

    public static Action<float> OnSpeedValue;
    public static Action<bool> OnSpeedIncreased;
    public static Action OnObstacleBumped;

    private void Awake()
    {
        SteerLeftController.OnLeftButtonPressed += SteerLeft;
        SteerRightController.OnRightButtonPressed += SteerRight;
        MenuButtons.OnCanMove += CanMoveMethod;
    }

    void Update()
    {
        OnSpeedValue?.Invoke(speed);

        if (canMove)
        {
            CarGoes();
        }
    }

    private void CarGoes()
    {
        if (speed >= maxSpeed)
        {
            acceleration = 0f;
        }

        speed += acceleration * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (isLeftButtonPressed)
        {
            transform.Rotate(0f, -steerDirectionValue * steerTurnSpeed * Time.deltaTime, 0f);
        }

        if (isRightButtonPressed)
        {
            transform.Rotate(0f, steerDirectionValue * steerTurnSpeed * Time.deltaTime, 0f);
        }
    }

    private void SteerLeft(bool flag)
    {
        isLeftButtonPressed = flag;
    }

    private void SteerRight(bool flag)
    {
        isRightButtonPressed = flag;
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
                //OnObstacleBumped?.Invoke();
                print("Bumped");
                break;
            case "SpeedIncrease":
                OnSpeedIncreased?.Invoke(true);
                StartCoroutine(IncreaseMaxSpeedSteerValue());
                break;
            case "SpeedReduce":
                OnSpeedIncreased?.Invoke(false);
                StartCoroutine(ReduceMaxSpeedSteerValue());
                break;
        }
    }

    IEnumerator IncreaseMaxSpeedSteerValue()
    {
        yield return new WaitForSeconds(1f);
        maxSpeed += 5f;
        acceleration = 2f;
        steerTurnSpeed += 10f;
    }

    IEnumerator ReduceMaxSpeedSteerValue()
    {
        yield return new WaitForSeconds(1f);
        speed -= 2.5f;
        acceleration = 0f;
        steerTurnSpeed -= 10f;
    }

    private void OnDestroy()
    {
        SteerLeftController.OnLeftButtonPressed -= SteerLeft;
        SteerRightController.OnRightButtonPressed -= SteerRight;
        MenuButtons.OnCanMove -= CanMoveMethod;
    }
}

