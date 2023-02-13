using ProjectShedule.GlobalSetting.Models;

namespace ProjectShedule.GlobalSetting.Base.ViewModel
{
    public class ConvertableSlideSettingViewModel : SlideSettingViewModel, ISlideValueConvert
    {
        private readonly BaseConvertableSlideSettingModel _convertableSlideSettingModel;

        public ConvertableSlideSettingViewModel(BaseConvertableSlideSettingModel convertableSlideSettingModel)
            : base(convertableSlideSettingModel)
        {
            _convertableSlideSettingModel = convertableSlideSettingModel;
        }

        public double GetConvertToDataValue() => _convertableSlideSettingModel.GetConvertToDataValue();
        public double GetConvertToSlideValue() => _convertableSlideSettingModel.GetConvertToSlideValue();
    }
}
