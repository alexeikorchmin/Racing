using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text carSpeedText;
    [SerializeField] private TMP_Text currentScoreResultText;

    private float currentScorePoints;
    private float carSpeedScoreMultiplier;
    private bool gameIsPlayed = false;

    private void Awake()
    {
        CarMovementController.OnSpeedValue += GetCarSpeed;
        CarMovementController.OnObstacleBumped += ShowCurrentScoreResult;
        GameManager.OnCanMove += OnCanMoveHandler;
    }

    private void Update()
    {
        if (gameIsPlayed)
        {
            ShowScoreAndSpeed();
        }
    }

    private void ShowScoreAndSpeed()
    {
        currentScorePoints += Time.deltaTime;
        currentScoreText.text = $"Score: {Mathf.FloorToInt(currentScorePoints).ToString()}";
        carSpeedText.text = $"{Mathf.FloorToInt(carSpeedScoreMultiplier * 3).ToString()} km/h";
    }

    private void GetCarSpeed(float carSpeed)
    {
        carSpeedScoreMultiplier = carSpeed;
    }

    private void ShowCurrentScoreResult()
    {
        currentScoreResultText.text = $"Current Score: {Mathf.FloorToInt(currentScorePoints).ToString()}";
    }

    private void OnCanMoveHandler(bool canMove)
    {
        gameIsPlayed = canMove;

        if (!canMove)
        {
            ShowCurrentScoreResult();
        }
    }

    private void OnDestroy()
    {
        CarMovementController.OnSpeedValue -= GetCarSpeed;
        CarMovementController.OnObstacleBumped -= ShowCurrentScoreResult;
        GameManager.OnCanMove -= OnCanMoveHandler;

        int currentBestScore = SavingSystem.GetBestScore();

        if (currentScorePoints > currentBestScore)
        {
            SavingSystem.SetBestScore(Mathf.FloorToInt(currentScorePoints));
        }
    }
}