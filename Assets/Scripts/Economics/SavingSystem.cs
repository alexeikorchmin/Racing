using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    private const string BestScoreKey = "BEST_SCORE";
    private const string EnergyKey = "ENERGY";
    private const string EnergyReadyTimeKey = "ENERGY_READY_TIME";

    public static void SetBestScore(int bestScore)
    {
        PlayerPrefs.SetInt(BestScoreKey, bestScore);
    }

    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    public static void SetEnergy(int energy)
    {
        PlayerPrefs.SetInt(EnergyKey, energy);
    }

    public static int GetEnergy(int defaultMaxEnergy)
    {
        return PlayerPrefs.GetInt(EnergyKey, defaultMaxEnergy);
    }

    public static void SetEnergyReadyTime(string time)
    {
        PlayerPrefs.SetString(EnergyReadyTimeKey, time);
    }

    public static string GetEnergyReadyTime()
    {
        return PlayerPrefs.GetString(EnergyReadyTimeKey, string.Empty);
    }
}