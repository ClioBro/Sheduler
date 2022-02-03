using ProjectShedule.GlobalSetting.Settings.AppTheme.Models;
using ProjectShedule.GlobalSetting.ViewModels;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.ViewModels
{
    public class SheduleThemeSettingViewModel : SettingViewModel
    {
        public SheduleThemeSettingViewModel()
        {
            Title = Resources.SettingResources.ThemeHeaderLabel;
            ThemeSetting = new DarkModeSettingModel();
        }
        public DarkModeSettingModel ThemeSetting { get; set; }
    }
}
