using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents.Models
{
    public class CornerRadiusEventSettingModel : SettingPushButtonModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public CornerRadiusEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = SettingResources.CornerRadiusDopTextLabel;
            Value = _shapeEventSetting.GetCornerRadius();
            MinValue = 0;
            MaxValue = 10;
            ValueChanged += OnValueChanged;
        }
        private void OnValueChanged(object sender, double e)
        {
            _shapeEventSetting.SetCornerRadius((float)e);
            OnPropertyChanged(this, nameof(Value));
        }
    }
}
