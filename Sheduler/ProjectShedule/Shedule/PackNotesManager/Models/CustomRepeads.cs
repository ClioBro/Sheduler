using ProjectShedule.Shedule.Enum;
using ProjectShedule.Shedule.ViewModels;

namespace ProjectShedule.Shedule.Models
{
    public static class CustomRepeads
    {
        static CustomRepeads()
        {
            Repeads = new Repead[]
            {
                   new Repead{ Text = "Без повтора", RepeadType = RepeadType.NoRepeat },
                   new Repead{ Text = "Каждый день", RepeadType = RepeadType.EveryDay },
                   new Repead{ Text = "Каждую неделю", RepeadType = RepeadType.EveryWeek },
                   new Repead{ Text = "Каждый месяц", RepeadType = RepeadType.EveryMonth },
                   new Repead{ Text = "Каждый год", RepeadType = RepeadType.EveryYear }
            };
        }
        public static Repead[] Repeads { get; }
    }
}
