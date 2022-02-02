using ProjectShedule.Shedule.NotifyOnApp.Enum;
using ProjectShedule.Shedule.Resources;

namespace ProjectShedule.Shedule.Models
{
    public static class CustomRepeads
    {
        static CustomRepeads()
        {
            RepeadsItems = new RepeadItem[]
            {
                   new RepeadItem{ Text = Repeads.NoRepeat, RepeadType = RepeadType.NoRepeat },
                   new RepeadItem{ Text = Repeads.EveryDay, RepeadType = RepeadType.EveryDay },
                   new RepeadItem{ Text = Repeads.EveryWeek, RepeadType = RepeadType.EveryWeek },
                   new RepeadItem{ Text = Repeads.EveryMonth, RepeadType = RepeadType.EveryMonth },
                   new RepeadItem{ Text = Repeads.EveryYear, RepeadType = RepeadType.EveryYear }
            };
        }
        public static RepeadItem[] RepeadsItems { get; }
    }
}
