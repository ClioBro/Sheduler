using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.Models
{
    public class SizeEventSettingModel : BaseConvertableSlideSettingModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;

        public SizeEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;

            MainText = SettingResources.SizeDopTextLabel;
            Value = GetConvertToSlideValue();
        }

        protected override double MaxDataValue => _shapeEventSetting.MaxSize;
        protected override double DataValue => _shapeEventSetting.GetSize().Height;

        public override void SaveSettings()
        {
            double result = GetConvertToDataValue();
            _shapeEventSetting.SetSize(result);
        }
    }
}
