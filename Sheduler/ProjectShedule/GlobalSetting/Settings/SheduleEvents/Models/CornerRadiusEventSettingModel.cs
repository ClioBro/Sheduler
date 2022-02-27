using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;
using System;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class CornerRadiusEventSettingModel : SlideSettingModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public CornerRadiusEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = SettingResources.CornerRadiusDopTextLabel;
            MaxValue = 100d;
            MinValue = 0d;
            Value = ConvertToValue(_shapeEventSetting.GetCornerRadius(), _shapeEventSetting.MaxCornerRadius);
            DragCompletedCommand = new Command(() => SaveOnMemory(Value));
        }
        private void SaveOnMemory(double value)
        {
            float result = (float)ConvertToMemory(value, _shapeEventSetting.MaxCornerRadius);
            _shapeEventSetting.SetCornerRadius(result);
        }
    }
}
