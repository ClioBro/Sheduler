using ProjectShedule.Core.Enum;
using System;

namespace ProjectShedule.Escaping
{
    public class Notification : INotification
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? AlertTime { get; set; }
        public RepeatType RepeatType { get; set; }
    }
}
