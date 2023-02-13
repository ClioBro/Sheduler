using ProjectShedule.Core.Enum;
using System;

namespace ProjectShedule.Escaping
{
    public interface INotification
    {
        public int ID { get; }
        string Title { get; }
        string Message { get; }
        DateTime? AlertTime { get; }
        RepeatType RepeatType { get; }
    }
}
