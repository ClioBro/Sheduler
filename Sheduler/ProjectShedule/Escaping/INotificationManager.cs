using System;

namespace ProjectShedule.Escaping
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void Initialize();
        public void SendNotification(Notification notify);
        void ReceiveNotification(Notification notify);
    }
}
