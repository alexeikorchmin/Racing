using UnityEngine;

public class PlaneControllerDestinations : MonoBehaviour
{
    [SerializeField] private Transform[] destinationGoals;
    [SerializeField] private GameObject planeProp;
    [SerializeField] private float expectedTimeToDestination = 10f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private float elapsedTime;
    private int currentGoalIndex = 0;
    private bool canMove = false;

    private void Awake()
    {
        SetStartingPosition();
        GameManager.OnCanMove += CanMoveMethod;
    }

    private void Update()
    {
        if (canMove)
        {
            planeProp.transform.Rotate(0f, 0f, +50f);

            FlyToDestination();
            SetNextDestinationGoal();
        }
    }

    private void CanMoveMethod(bool flag)
    {
        canMove = flag;
    }

    private void SetStartingPosition()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void SetNextDestinationGoal()
    {
        if (transform.position == destinationGoals[currentGoalIndex].position)
        {
            elapsedTime = 0;
            currentGoalIndex = ++currentGoalIndex;

            SetStartingPosition();

            if (currentGoalIndex == destinationGoals.Length)
                currentGoalIndex = 0;
        }
    }

    private void FlyToDestination()
    {
        elapsedTime += Time.deltaTime;
        float completedPath = elapsedTime / expectedTimeToDestination;

        transform.position = Vector3.Lerp(startPosition, destinationGoals[currentGoalIndex].position, completedPath);
        transform.rotation = Quaternion.Lerp(startRotation, destinationGoals[currentGoalIndex].rotation, completedPath * 5);
    }

    private void OnDestroy()
    {
        GameManager.OnCanMove -= CanMoveMethod;
    }
}