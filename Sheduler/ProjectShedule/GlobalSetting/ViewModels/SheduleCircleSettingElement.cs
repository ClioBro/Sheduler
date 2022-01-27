using System;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class SheduleCircleSettingElement : DoubleSettingViewModel
    {
        private readonly ShapeEventSetting _circleSetting;
        private double _currentOpacity;
        public SheduleCircleSettingElement()
        {
            _circleSetting = new ShapeEventSetting();
            Title = "ShapeEvent:";
            MainText = "Opacity:";
            MaxValue = 1;
            MinValue = 0.0;
            _currentOpacity = _circleSetting.GetOpacity();
            Value = _currentOpacity;
            ActionChangedDoubleValue += OnDoubleValueChanged;
        }
        private void OnDoubleValueChanged(double value)
        {
            _circleSetting.SetOpacity(value);
            OnPropertyChanged(this, nameof(Value));
        }
    }
}