using ProjectShedule.GlobalSetting.Models;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class OpacityEventSettingModel : SettingPushButtonModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public OpacityEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = Resources.SettingResources.OpacityDopTextLabel;
            Value = _shapeEventSetting.GetOpacity();
            MinValue = 0;
            MaxValue = 1;
            ValueChanged += OnValueChanged;
        }
        private void OnValueChanged(object sender, double e)
        {
            _shapeEventSetting.SetOpacity(e);
            OnPropertyChanged(this, nameof(Value));
        }
    }
}
