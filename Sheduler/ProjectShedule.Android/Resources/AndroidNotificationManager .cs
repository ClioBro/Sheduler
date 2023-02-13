using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using ProjectShedule.Droid;
using ProjectShedule.Escaping;
using System;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;
using LanguageResource = ProjectShedule.Language.Resources.OtherElements;

[assembly: Dependency(typeof(ProjectShedule._0.Droid.Resources.AndroidNotificationManager))]
namespace ProjectShedule._0.Droid.Resources
{
    public class AndroidNotificationManager : INotificationManager
    {
        private const string channelId = "default";
        private const string channelName = "Default";
        private const string channelDescription = "The default channel for notifications.";

        public const string IDKey = "idKey";
        public const string TitleKey = "title";
        public const string MessageKey = "message";
        public const string RepeatKey = "repeat";
        public const string DateTimeAlertKey = "dateTimeAlert";


        private bool channelInitialized = false;
        private int messageId = 0;

        private NotificationManager manager;

        public event EventHandler NotificationReceived;

        public static AndroidNotificationManager Instance { get; private set; }

        public AndroidNotificationManager() => Initialize();

        public void Initialize()
        {
            if (Instance == null)
            {
                CreateNotificationChannel();
                Instance = this;
            }
        }
        public void Send(INotification notify)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            if (notify.AlertTime != null)
            {
                long triggerTime = GetNotifyTime(notify.AlertTime.Value);

                Intent intent = new Intent(AndroidApp.Context, typeof(AlarmHandler));
                intent.PutExtra(IDKey, notify.ID);
                intent.PutExtra(TitleKey, notify.Title);
                intent.PutExtra(MessageKey, notify.Message);
                intent.PutExtra(RepeatKey, (int)notify.RepeatType);
                intent.PutExtra(DateTimeAlertKey, triggerTime);

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, notify.ID, intent, PendingIntentFlags.CancelCurrent);
                AlarmManager alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
                alarmManager.SetExact(AlarmType.Rtc, triggerTime, pendingIntent);

            }
            else
            {
                Show(notify);
            }
        }
        public void Cancel(INotification notify)
        {
            if (!channelInitialized)
                CreateNotificationChannel();

            AlarmManager alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            Intent myIntent = new Intent(AndroidApp.Context, typeof(AlarmHandler));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, notify.ID, myIntent, PendingIntentFlags.UpdateCurrent);
            alarmManager.Cancel(pendingIntent);
        }

        public void Receive(INotification notification)
        {
            var args = new NotificationEventArgs()
            {
                Notify = notification
            };
            NotificationReceived?.Invoke(this, args);
        }

        public void Show(INotification notify)
        {
            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, notify.Title);
            intent.PutExtra(MessageKey, notify.Message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, notify.ID, intent, PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle($"{LanguageResource.AppNotification.Title} {notify.Title}")
                .SetContentText(LanguageResource.AppNotification.Message)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.notification_icon))
                .SetSmallIcon(Resource.Drawable.note_icon_negate)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            Android.App.Notification androidAppNotification = notificationBuilder.Build();
            manager.Notify(messageId++, androidAppNotification);
        }

        private void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Max)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }

        private long GetNotifyTime(DateTime notifyTime)
        {
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
            return utcAlarmTime; // milliseconds
        }
    }
}