namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class SheduleCircleOpacitySetting : DoubleSettingViewModel
    {
        private readonly ShapeEventSetting _circleSetting;
        private double _currentOpacity;
        public SheduleCircleOpacitySetting()
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
    public class SheduleCircleCorcnerRadiusSetting : FloatSettingViewModel
    {
        private readonly ShapeEventSetting _circleSetting;
        private float _currentCornerRadius;
        public SheduleCircleCorcnerRadiusSetting()
        {
            _circleSetting = new ShapeEventSetting();
            Title = "ShapeEvent:";
            MainText = "CornerRadius:";
            MaxValue = 10f;
            MinValue = 0f;
            _currentCornerRadius = _circleSetting.GetCornerRadius();
            Value = _currentCornerRadius;
            ActionChangedDoubleValue += OnFloatValueChanged;
        }
        private void OnFloatValueChanged(float value)
        {
            _circleSetting.SetCornerRadius(value);
            OnPropertyChanged(this, nameof(Value));
        }
    }
}