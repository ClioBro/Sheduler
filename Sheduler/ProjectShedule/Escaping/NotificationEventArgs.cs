using System;

namespace ProjectShedule.Escaping
{
    public class NotificationEventArgs : EventArgs
    {
        public INotification Notify { get; set; }
    }
}
