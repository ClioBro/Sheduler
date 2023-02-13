using ProjectShedule.Core.Enum;

namespace ProjectShedule.Core.Notify
{
    public class NotifyRepeat
    {
        public NotifyRepeat(RepeatType repeatType, string text)
        {
            RepeatType = repeatType;
            Text = text;
        }
        public string Text { get; }
        public RepeatType RepeatType { get; }
    }

}
