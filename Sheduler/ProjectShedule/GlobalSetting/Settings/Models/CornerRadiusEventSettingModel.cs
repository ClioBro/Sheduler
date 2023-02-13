using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.Models
{
    public class CornerRadiusEventSettingModel : BaseConvertableSlideSettingModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public CornerRadiusEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;

            MainText = SettingResources.CornerRadiusDopTextLabel;
            Value = GetConvertToSlideValue();
        }

        protected override double MaxDataValue => _shapeEventSetting.MaxCornerRadius;
        protected override double DataValue => _shapeEventSetting.GetCornerRadius();

        public override void SaveSettings()
        {
            float result = (float)GetConvertToDataValue();
            _shapeEventSetting.SetCornerRadius(result);
        }
    }
}
