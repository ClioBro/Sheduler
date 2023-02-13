using ProjectShedule.Core;
using ProjectShedule.GlobalSetting.Settings.ViewModels;

namespace ProjectShedule.GlobalSetting
{
    public class SettingPageViewModel : BaseViewModel
    {
        public SettingPageViewModel()
        {
            ThemeSettingViewModel = new ThemeSettingViewModel();
            SheduleDeleteSettingViewModel = new SheduleDeleteSettingViewModel();
            SheduleEventsSettingViewModel = new SheduleEventsSettingViewModel();
            CarouselSettingsViewModel= new CarouselSettingsViewModel();
        }

        public ThemeSettingViewModel ThemeSettingViewModel { get; private set; }
        public SheduleDeleteSettingViewModel SheduleDeleteSettingViewModel { get; private set; }
        public SheduleEventsSettingViewModel SheduleEventsSettingViewModel { get; private set; }
        public CarouselSettingsViewModel CarouselSettingsViewModel { get; set; }
    }
}
