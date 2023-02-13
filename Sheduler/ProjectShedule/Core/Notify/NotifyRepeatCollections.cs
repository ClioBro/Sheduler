using ProjectShedule.Core.Enum;
using PopUpResources = ProjectShedule.Language.Resources.PopUp.Repeads;

namespace ProjectShedule.Core.Notify
{
    public class NotifyRepeatCollections
    {
        public static NotifyRepeat[] NotifyRepeats { get; } = new NotifyRepeat[]
        {
            new NotifyRepeat(RepeatType.NoRepeat, PopUpResources.Repeads.NoRepeat),
            new NotifyRepeat(RepeatType.EveryDay, PopUpResources.Repeads.EveryDay),
            new NotifyRepeat(RepeatType.EveryWeek, PopUpResources.Repeads.EveryWeek),
            new NotifyRepeat(RepeatType.EveryMonth, PopUpResources.Repeads.EveryMonth),
            new NotifyRepeat(RepeatType.EveryYear, PopUpResources.Repeads.EveryYear),
        };
    }
}
