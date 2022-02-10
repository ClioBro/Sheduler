using ProjectShedule.GlobalSetting.Resource;
using ProjectShedule.GlobalSetting.Settings.AppTheme.Models;
using ProjectShedule.GlobalSetting.ViewModels;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.ViewModels
{
    public class SheduleThemeSettingViewModel : SettingViewModel
    {
        public SheduleThemeSettingViewModel()
        {
            Title = SettingResources.ThemeHeaderLabel;
            ThemeSetting = new DarkModeSettingModel();
        }
        public DarkModeSettingModel ThemeSetting { get; set; }
    }
}
