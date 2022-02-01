﻿using ProjectShedule.GlobalSetting.Models;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class SizeEventSettingModel : SettingPushButtonModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public SizeEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = "Size:";
            Value = _shapeEventSetting.GetSize().Height;
            MinValue = 0;
            MaxValue = 10;
            ValueChanged += OnValueChanged;
        }
        private void OnValueChanged(object sender, double e)
        {
            _shapeEventSetting.SetSize(e);
            OnPropertyChanged(this, nameof(Value));
        }
    }
}