using ProjectShedule.GlobalSetting.Settings.AppTheme.Models;
using ProjectShedule.GlobalSetting.ViewModels;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.ViewModels
{
    public class SheduleThemeSettingViewModel : SettingViewModel
    {
        private ThemeController _themeController;
        public SheduleThemeSettingViewModel()
        {
            _themeController = App.ThemeController;
            Title = "Theme:";
            ThemeSetting = new DarkModeSettingModel(_themeController);
        }
        public DarkModeSettingModel ThemeSetting { get; set; }
    }
}
