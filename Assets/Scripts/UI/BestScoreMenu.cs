using UnityEngine;
using TMPro;

public class BestScoreMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text BestScoreText;

    private void Start()
    {
        int bestScore = PlayerPrefs.GetInt(ScoreSystem.BestScoreKey, 0);

        BestScoreText.text = $"{ScoreSystem.BestScoreKey} {bestScore}";
    }
}