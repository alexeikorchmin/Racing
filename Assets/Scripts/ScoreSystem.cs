using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text carSpeedText;
    [SerializeField] private TMP_Text CurrentScoreResultText;

    public const string BestScoreKey = "Best Score:";

    private float scorePoints;
    private float carSpeedScoreMultiplier;
    private bool gameIsPlayed = false;


    private void Awake()
    {
        CarMovementController.OnSpeedValue += GetCarSpeed;
        CarMovementController.OnObstacleBumped += ShowCurrentScoreResult;
        MenuButtons.OnCanMove += gameIsPlayedMethod;
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
        scorePoints += Time.deltaTime;
        currentScoreText.text = $"Score: {Mathf.FloorToInt(scorePoints).ToString()}";
        carSpeedText.text = $"{Mathf.FloorToInt(carSpeedScoreMultiplier * 3).ToString()} km/h";
    }

    private void GetCarSpeed(float carSpeed)
    {
        carSpeedScoreMultiplier = carSpeed;
    }

    private void ShowCurrentScoreResult()
    {
        CurrentScoreResultText.text = $"Current Score: {Mathf.FloorToInt(scorePoints).ToString()}";
    }

    private void gameIsPlayedMethod(bool flag)
    {
        gameIsPlayed = flag;
    }

    private void OnDestroy()
    {
        CarMovementController.OnSpeedValue -= GetCarSpeed;
        CarMovementController.OnObstacleBumped -= ShowCurrentScoreResult;
        MenuButtons.OnCanMove -= gameIsPlayedMethod;

        int currentBestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        if (scorePoints > currentBestScore)
        {
            PlayerPrefs.SetInt(BestScoreKey, Mathf.FloorToInt(scorePoints));
        }
    }
}
