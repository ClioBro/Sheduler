using Android.Content;
using ProjectShedule.Core;
using ProjectShedule.Core.Enum;
using ProjectShedule.Core.Notify;
using ProjectShedule.Escaping;

namespace ProjectShedule._0.Droid.Resources
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broatcast Reciver")]
    public class AlarmHandler : BroadcastReceiver
    {
        AndroidNotificationManager _androidNotifymanager;
        Notification _notification;
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Extras == null)
                return;

            int id = intent.GetIntExtra(AndroidNotificationManager.IDKey, 0);
            string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
            string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
            int repeat = intent.GetIntExtra(AndroidNotificationManager.RepeatKey, 0);
            long alertTime = intent.GetLongExtra(AndroidNotificationManager.DateTimeAlertKey, 0);

            _notification = new Notification()
            {
                ID = id,
                Title = title,
                Message = message,
                RepeatType = (RepeatType)repeat,
                AlertTime = DateTimeExtensions.LongInDateTime(alertTime)
            };

            _androidNotifymanager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
            _androidNotifymanager.Receive(_notification);
            _androidNotifymanager.Show(_notification);

            if (_notification.RepeatType != RepeatType.NoRepeat)
                SetRepeatNotify();
        }
        private void SetRepeatNotify()
        {
            RepeatNotifyManager repeatNotifyManager = new RepeatNotifyManager(_notification);
            repeatNotifyManager.SetNewAlertTime();
            _androidNotifymanager.Send(_notification);
        }
    }
}