using ProjectShedule.GlobalSetting.Settings.AppTheme.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.ViewModels
{
    public class SheduleThemeSettingViewModel : SettingBoxViewModel
    {
        public SheduleThemeSettingViewModel()
        {
            Header = SettingResources.ThemeHeaderLabel;
            ThemeSetting = new DarkModeSettingModel();
        }
        public DarkModeSettingModel ThemeSetting { get; set; }
    }
}
