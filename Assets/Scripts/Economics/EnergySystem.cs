using System;
using UnityEngine;
using TMPro;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] private TMP_Text currentEnergyText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeTime;

    private int currentEnergy;

    private void Start()
    {
        CheckEnergy();
    }

    private void CheckEnergy()
    {
        currentEnergy = SavingSystem.GetEnergy(maxEnergy);

        if (currentEnergy == 0)
        {
            string energyReadyTimeString = SavingSystem.GetEnergyReadyTime();

            if (energyReadyTimeString == string.Empty) { return; }

            DateTime energyReadyTime = DateTime.Parse(energyReadyTimeString);

            if (DateTime.Now > energyReadyTime)
            {
                currentEnergy = maxEnergy;
                SavingSystem.SetEnergy(currentEnergy);
            }
        }

        currentEnergyText.text = $"Current Energy: {currentEnergy}";
    }

    public bool CheckIsEnoughEnergy()
    {
        if (currentEnergy < 1)
        {
            return false;
        }
        else
        {
            currentEnergy--;

            SavingSystem.SetEnergy(currentEnergy);

            if (currentEnergy == 0)
            {
                DateTime energyReady = DateTime.Now.AddSeconds(energyRechargeTime);
                SavingSystem.SetEnergyReadyTime(energyReady.ToString());
            }

            currentEnergyText.text = $"Current Energy: {currentEnergy}";

            return true;
        }
    }
}