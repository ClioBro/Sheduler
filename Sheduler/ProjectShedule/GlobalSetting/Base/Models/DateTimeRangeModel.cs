using ProjectShedule.Core;
using ProjectShedule.GlobalSetting.Models;
using System;

namespace ProjectShedule.GlobalSetting.Base.Models
{
    public abstract class DateTimeRangeModel : BaseElementCellModel<DateTimeRange>
    {
        public abstract DateTime MaxStart { get; }
        public abstract DateTime MinStart { get; }
        public abstract DateTime MaxEnd { get; }
        public abstract DateTime MinEnd { get; }
    }
}
