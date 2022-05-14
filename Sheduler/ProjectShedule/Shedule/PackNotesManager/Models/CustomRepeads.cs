using PopUpResources = ProjectShedule.Language.Resources.PopUp.Repeads ;
using ProjectShedule.Shedule.NotifyOnApp.Enum;

namespace ProjectShedule.Shedule.Models
{
    public static class CustomRepeads
    {
        static CustomRepeads()
        {
            RepeadsItems = new RepeadItem[]
            {
                   new RepeadItem{ Text = PopUpResources.Repeads.NoRepeat, RepeadType = RepeadType.NoRepeat },
                   new RepeadItem{ Text = PopUpResources.Repeads.EveryDay, RepeadType = RepeadType.EveryDay },
                   new RepeadItem{ Text = PopUpResources.Repeads.EveryWeek, RepeadType = RepeadType.EveryWeek },
                   new RepeadItem{ Text = PopUpResources.Repeads.EveryMonth, RepeadType = RepeadType.EveryMonth },
                   new RepeadItem{ Text = PopUpResources.Repeads.EveryYear, RepeadType = RepeadType.EveryYear }
            };
        }
        public static RepeadItem[] RepeadsItems { get; }
    }
}
