using ProjectShedule.Core;
using ProjectShedule.GlobalSetting.Base.Models;
using ProjectShedule.GlobalSetting.Settings.YearsRange;
using System;

namespace ProjectShedule.GlobalSetting.Settings.Models
{
    public class YearsRangeModel : DateTimeRangeModel
    {
        private YearsRangeSetting _yearsRangeSetting;
        public YearsRangeModel()
        {
            _yearsRangeSetting = new YearsRangeSetting();
            MainText = Language.Resources.Pages.Setting.SettingResources.YearsRangeLabel;
        }

        public override DateTime MaxStart => new DateTime(_yearsRangeSetting.MaxStart, 1, 1);
        public override DateTime MinStart => new DateTime(_yearsRangeSetting.MinStart, 1, 1);
        public override DateTime MaxEnd => new DateTime(_yearsRangeSetting.MaxEnd, 1, 1);
        public override DateTime MinEnd => new DateTime(_yearsRangeSetting.MinEnd, 1, 1);

        public override DateTimeRange Value
        {
            get => GetDateTimeRangeByYears();
            set
            {
                _yearsRangeSetting.Start = value.Start.Year;
                _yearsRangeSetting.End = value.End.Year;
            }
        }

        private DateTimeRange GetDateTimeRangeByYears()
        {
            var start = new DateTime(_yearsRangeSetting.Start, 1, 1);
            var end = new DateTime(_yearsRangeSetting.End, 1, 1);
            return new DateTimeRange(start, end);
        }
    }
}
