using ProjectShedule.GlobalSetting.Base;

namespace ProjectShedule.GlobalSetting.Models
{
    public abstract class BaseConvertableSlideSettingModel : BaseSlideSettingModel, ISlideValueConvert
    {
        protected abstract double MaxDataValue { get; }
        protected abstract double DataValue { get; }

        public double GetConvertToDataValue()
        {
            return PercentConverter.DeConvertValue(Value, MaxDataValue, percentValue: MaxValue);
        }
        public double GetConvertToSlideValue()
        {
            return PercentConverter.ConvertToValue(DataValue, MaxDataValue, percentValue: MaxValue);
        }
    }
}
