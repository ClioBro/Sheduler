using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class OpacityEventSettingModel : DoubleValueElementCell
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public OpacityEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = SettingResources.OpacityDopTextLabel;
            MaxValue = 100d;
            MinValue = 0d;
            Value = PercentConverter.DeConvert(_shapeEventSetting.GetOpacity(), _shapeEventSetting.MaxOpacity);
            ValueChanged += OnValueChanged;
        }
        private void OnValueChanged(object sender, double e)
        {
            double value = PercentConverter.Convert(e, _shapeEventSetting.MaxOpacity);
            _shapeEventSetting.SetOpacity(value);
            NotifyVisualUpdate(this, nameof(Value));
        }
    }
}
