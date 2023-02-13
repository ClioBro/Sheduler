using ProjectShedule.GlobalSetting.Base.ViewModel;
using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Settings.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.ViewModels
{
    public class ThemeSettingViewModel : SettingBoxViewModel
    {
        public ThemeSettingViewModel()
        {
            Header = SettingResources.ThemeHeaderLabel;
            ThemeSwitchSetting = new SwitchSettingViewModel(new DarkModeSettingModel());
        }

        public SwitchSettingViewModel ThemeSwitchSetting { get; set; }
    }
}
