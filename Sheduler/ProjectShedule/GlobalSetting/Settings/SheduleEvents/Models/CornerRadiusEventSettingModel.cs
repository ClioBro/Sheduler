using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;
using System;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class CornerRadiusEventSettingModel : DoubleValueElementCell
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public CornerRadiusEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = SettingResources.CornerRadiusDopTextLabel;
            MaxValue = 100d;
            MinValue = 0d;
            double result = PercentConverter.DeConvert(_shapeEventSetting.GetCornerRadius(), _shapeEventSetting.MaxCornerRadius);
            Value = Math.Round(result, 1);
            ValueChanged += OnValueChanged;
        }

        private void OnValueChanged(object sender, double e)
        {
            float result = (float)PercentConverter.Convert(e, _shapeEventSetting.MaxCornerRadius);
            _shapeEventSetting.SetCornerRadius(result);
            NotifyVisualUpdate(this, nameof(Value));
        }
    }
}
