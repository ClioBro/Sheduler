using ProjectShedule.GlobalSetting.Base.ViewModel;
using ProjectShedule.GlobalSetting.Settings.Models;
using ProjectShedule.GlobalSetting.ViewModel;

namespace ProjectShedule.GlobalSetting.Settings.ViewModels
{
    public class CarouselSettingsViewModel : SettingBoxViewModel
    {
        public CarouselSettingsViewModel()
        {
            Header = Language.Resources.Pages.Setting.SettingResources.CarouselHeaderLabel;
            YearsRangeSettingViewModel = new YearsRangeViewModel(new YearsRangeModel());
        }

        public DateTimeRangeVewModel YearsRangeSettingViewModel { get; set; }
    }
}
