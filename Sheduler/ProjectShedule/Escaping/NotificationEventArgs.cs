using ProjectShedule.Shedule.NotifyOnApp.Enum;
using System;

namespace ProjectShedule.Escaping
{
    public class NotificationEventArgs : EventArgs
    {
        public Notification Notify { get; set; }
    }

    public class Notification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? AlertTime { get; set; }
        public RepeadType RepeadType { get; set; }
    }
}
