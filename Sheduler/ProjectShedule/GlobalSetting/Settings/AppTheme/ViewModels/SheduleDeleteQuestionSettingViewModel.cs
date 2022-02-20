using ProjectShedule.GlobalSetting.Interfaces;
using ProjectShedule.GlobalSetting.Settings.AppTheme.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.ViewModels
{
    public class SheduleThemeSettingViewModel : SettingBox
    {
        public SheduleThemeSettingViewModel()
        {
            Header = SettingResources.ThemeHeaderLabel;
            ThemeSetting = new DarkModeSettingModel();
        }
        public IElementCell<bool> ThemeSetting { get; set; }
    }
}
