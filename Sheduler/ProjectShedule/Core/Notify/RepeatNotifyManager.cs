using ProjectShedule.Core.Enum;
using ProjectShedule.Escaping;
using System;

namespace ProjectShedule.Core.Notify
{
    public class RepeatNotifyManager
    {
        readonly Notification _notification;
        public RepeatNotifyManager(Notification notification)
        {
            _notification = notification;
        }
        public void SetNewAlertTime()
        {
            _notification.AlertTime = AddDaysByRepeatType(_notification.AlertTime.Value).ToLocalTime();
        }
        private DateTime AddDaysByRepeatType(DateTime dateTime)
        {
            return _notification.RepeatType switch
            {
                RepeatType.EveryDay => dateTime.AddDays(1),
                RepeatType.EveryWeek => dateTime.AddDays(7),
                RepeatType.EveryMonth => dateTime.AddMonths(1),
                RepeatType.EveryYear => dateTime.AddYears(1),
                _ => dateTime
            };
        }
    }
}