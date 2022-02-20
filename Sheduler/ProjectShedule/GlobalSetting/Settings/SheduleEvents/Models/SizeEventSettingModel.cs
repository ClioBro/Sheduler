using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class SizeEventSettingModel : DoubleValueElementCell
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public SizeEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = SettingResources.SizeDopTextLabel;
            MaxValue = 100d;
            MinValue = 0d;
            Value = PercentConverter.DeConvert(_shapeEventSetting.GetSize().Height, _shapeEventSetting.MaxSize);
            ValueChanged += OnValueChanged;
        }
        private void OnValueChanged(object sender, double e)
        {
            double value = PercentConverter.Convert(e, _shapeEventSetting.MaxSize);
            _shapeEventSetting.SetSize(value);
            NotifyVisualUpdate(this, nameof(Value));
        }
    }
}
