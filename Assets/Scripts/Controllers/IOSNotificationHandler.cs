using UnityEngine;
using Unity.Notifications.iOS;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class IOSNotificationHandler : MonoBehaviour
{
    //#if UNITY_IOS

    public void ScheduleNotification(int seconds)
    {
        iOSNotification notification = new iOSNotification
        {
            Title = "Energy's Recharged!",
            Subtitle = "Energy has been recharged!",
            Body = "Your car is ready for the race!",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread",
            Trigger = new iOSNotificationTimeIntervalTrigger
            {
                TimeInterval = new System.TimeSpan(0, 0, seconds),
                Repeats = false 
            }
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
    //#endif
}