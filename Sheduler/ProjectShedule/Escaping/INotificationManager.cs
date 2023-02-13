using System;

namespace ProjectShedule.Escaping
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void Initialize();
        void Send(INotification notify);
        void Cancel(INotification notify);
        void Receive(INotification notify);
    }
}
