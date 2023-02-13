using ProjectShedule.Core;
using ProjectShedule.Core.Stepper;
using ProjectShedule.GlobalSetting.Base.Models;

namespace ProjectShedule.GlobalSetting.Base.ViewModel
{
    public abstract class DateTimeRangeVewModel : BaseViewModel, IElementCell<DateTimeRange>
    {
        private readonly DateTimeRangeModel _dateTimeRangeModel;

        public DateTimeRangeVewModel(DateTimeRangeModel dateTimeRangeModel)
        {
            _dateTimeRangeModel = dateTimeRangeModel;
        }

        public string MainText => _dateTimeRangeModel.MainText;

        public string StartText => Language.Resources.Pages.Setting.SettingResources.StartLabel;
        public string EndText => Language.Resources.Pages.Setting.SettingResources.EndLabel;

        public abstract CustomStepperViewModel CustomStartStepperViewModel { get; set; }
        public abstract CustomStepperViewModel CustomEndStepperViewModel { get; set; }
        public DateTimeRange Value
        {
            get => _dateTimeRangeModel.Value;
            set
            {
                if (_dateTimeRangeModel.Value == value)
                    return;
                _dateTimeRangeModel.Value = value;
                OnPropertyChanged();
            }
        }
    }
}