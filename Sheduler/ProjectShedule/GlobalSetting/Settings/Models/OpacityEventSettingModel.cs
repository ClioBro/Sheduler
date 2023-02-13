using ProjectShedule.GlobalSetting.Base;
using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.Models
{
    public class OpacityEventSettingModel : BaseConvertableSlideSettingModel, ISlideValueConvert
    {
        private readonly ShapeEventSetting _shapeEventSetting;

        public OpacityEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;

            MainText = SettingResources.OpacityDopTextLabel;
            MaxValue = 100d;
            MinValue = 0d;
            Value = GetConvertToSlideValue();
        }

        protected override double MaxDataValue => _shapeEventSetting.MaxOpacity;
        protected override double DataValue => _shapeEventSetting.GetOpacity();

        public override void SaveSettings()
        {
            double result = GetConvertToDataValue();
            _shapeEventSetting.SetOpacity(result);
        }
    }
}
