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

    public class FloatSettingViewModel : SettingElementViewModel
    {
        public float MaxValue { get; set; }
        public float MinValue { get; set; }

        private float _settingValue;

        public Action<float> ActionChangedDoubleValue;
        public float Value
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