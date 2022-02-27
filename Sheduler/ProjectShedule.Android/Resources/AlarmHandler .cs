using Android.Content;
using ProjectShedule.Escaping;
using ProjectShedule.Shedule.NotifyOnApp.Enum;
using System;

namespace ProjectShedule._0.Droid.Resources
{
    [BroadcastReceiver(Enabled =true, Label = "Local Notifications Broatcast Reciver")]
    public class AlarmHandler : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                int repead = intent.GetIntExtra(AndroidNotificationManager.RepeadKey, 0);
                long alertTime = intent.GetLongExtra(AndroidNotificationManager.DateTimeAlertKey, 0);

                Notification notify = new Notification()
                {
                    Title = title,
                    Message = message,
                    RepeadType = (RepeadType)repead,
                    AlertTime = DateTimeExtension.LongInDateTime(alertTime)
                };

                AndroidNotificationManager manager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
                manager.Show(notify);

                if (notify.RepeadType != RepeadType.NoRepeat)
                {
                    RepeadNotifyManager.SetNewAlertTime(notify);
                    manager.SendNotification(notify);
                }
            }
        }
    }

    public abstract class RepeadNotifyManager
    {
        private const long dayOnMilliseconds = 86400000;
        private const long weekOnMilliseconds = 604800000;
        private const long monthOnMilliseconds = 2678400000;
        private const long yearOnMilliseconds = 31536000000;
        public static void SetNewAlertTime(Notification notification)
        {
            notification.AlertTime = ChangeDepending(notification.AlertTime.Value, notification.RepeadType);
        }
        private static DateTime ChangeDepending(DateTime dateTime, RepeadType repeadType)
        {
            return dateTime.AddMilliseconds(GetMilliseconds(repeadType)).ToLocalTime();
        }
        private static long GetMilliseconds(RepeadType repeadType)
        {
            return repeadType switch
            {
                RepeadType.EveryDay => dayOnMilliseconds,
                RepeadType.EveryWeek => weekOnMilliseconds,
                RepeadType.EveryMonth => monthOnMilliseconds,
                RepeadType.EveryYear => yearOnMilliseconds,
                _ => 0
            };
        }
    }
    public static class DateTimeExtension
    {
        public static DateTime LongInDateTime(long longDateTime)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(longDateTime).ToLocalTime();
        }
    }
}