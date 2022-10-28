using System;
using UnityEngine;
using TMPro;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private IOSNotificationHandler iOSNotificationHandler;
    [SerializeField] private TMP_Text currentEnergyText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeTime;

    private int currentEnergy;

    public bool CheckIsEnoughEnergy()
    {
        if (currentEnergy < 1) return false;

        currentEnergy--;

        if (currentEnergy == 0)
        {
            var energyWillBeReady = DateTime.Now.AddSeconds(energyRechargeTime);
            SavingSystem.SetEnergyReadyTime(energyWillBeReady.ToString());

#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyWillBeReady);
#elif UNITY_IOS
            IOSNotificationHandler.ScheduleNotification(energyRechargeTime);
#endif
        }

        SaveAndShowCurrentEnergy(currentEnergy);
        return true;
    }

    private void Start()
    {
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) return;

        CancelInvoke();

        SaveAndShowCurrentEnergy(SavingSystem.GetEnergy(maxEnergy));

        if (currentEnergy != 0) return;

        string energyReadyTimeString = SavingSystem.GetEnergyReadyTime();

        if (string.IsNullOrEmpty(energyReadyTimeString)) return;

        var energyReadyTime = DateTime.Parse(energyReadyTimeString);

        if (DateTime.Now > energyReadyTime)
        {
            EnergyRecharged();
        }
        else
        {
            Invoke(nameof(EnergyRecharged), (energyReadyTime - DateTime.Now).Seconds);
        }
    }

    private void EnergyRecharged()
    {
        currentEnergy = maxEnergy;
        SaveAndShowCurrentEnergy(currentEnergy);
    }

    private void SaveAndShowCurrentEnergy(int energy)
    {
        currentEnergy = energy;
        SavingSystem.SetEnergy(energy);
        currentEnergyText.text = $"Current Energy: {energy}";
    }
}