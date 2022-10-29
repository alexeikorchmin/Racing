using System;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID

    private const string channelId = "NOTIFICATION_CHANNEL";

    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = channelId,
            Name = "Notification Channel",
            Description = "Description",
            Importance = Importance.Default,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy's Recharged!",
            Text = "Your car is ready for the race!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime,
        };

        AndroidNotificationCenter.SendNotification(notification, channelId);
    }
#endif
}