using System;
using System.Windows.Input;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class DoubleSettingViewModel : SettingElementViewModel
    {
        public double MaxValue { get; set; }
        public double MinValue { get; set; }

        private double _settingValue;
        
        public Action<double> ActionChangedDoubleValue;
        public double Value
        {
            get => _settingValue;
            set
            {
                if (value <= MaxValue && value >= MinValue)
                {
                    _settingValue = value;
                    ActionChangedDoubleValue?.Invoke(value);
                }
            }
        }
    }
}