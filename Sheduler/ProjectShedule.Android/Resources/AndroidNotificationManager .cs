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

[assembly: Dependency(typeof(ProjectShedule._0.Droid.Resources.AndroidNotificationManager))]
namespace ProjectShedule._0.Droid.Resources
{

    public class AndroidNotificationManager : INotificationManager
    {
        private const string channelId = "default";
        private const string channelName = "Default";
        private const string channelDescription = "The default channel for notifications.";

        public const string TitleKey = "title";
        public const string MessageKey = "message";
        public const string RepeadKey = "repead";
        public const string DateTimeAlertKey = "dateTimeAlert";


        private bool channelInitialized = false;
        private int messageId = 0;
        private int pendingIntentId = 0;
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
        public void SendNotification(Escaping.Notification notify)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            if (notify.AlertTime != null)
            {
                long triggerTime = GetNotifyTime(notify.AlertTime.Value);

                Intent intent = new Intent(AndroidApp.Context, typeof(AlarmHandler));
                intent.PutExtra(TitleKey, notify.Title);
                intent.PutExtra(MessageKey, notify.Message);
                intent.PutExtra(RepeadKey, (int)notify.RepeadType);
                intent.PutExtra(DateTimeAlertKey, triggerTime);

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId++, intent, PendingIntentFlags.CancelCurrent);
                AlarmManager alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
                alarmManager.SetExact(AlarmType.Rtc, triggerTime, pendingIntent);
            }
            else
            {
                Show(notify);
            }
        }

        public void ReceiveNotification(Escaping.Notification notification)
        {
            var args = new NotificationEventArgs()
            {
                Notify = notification
            };
            NotificationReceived?.Invoke(this, args);
        }

        public void Show(Escaping.Notification notify)
        {
            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, notify.Title);
            intent.PutExtra(MessageKey, notify.Message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId++, intent, PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(notify.Title)
                .SetContentText(notify.Message)
                .SetColor(Resource.Color.material_blue_grey_950)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.pluse2_icon))
                .SetSmallIcon(Resource.Drawable.pluse2_icon)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            Android.App.Notification notification = builder.Build();
            manager.Notify(messageId++, notification);
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