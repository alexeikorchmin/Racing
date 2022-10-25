using UnityEngine;
using TMPro;

public class BestScoreMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text BestScoreText;

    private void Start()
    {
        int bestScore = SavingSystem.GetBestScore();
        BestScoreText.text = $"Best Score: {bestScore}";
    }
}