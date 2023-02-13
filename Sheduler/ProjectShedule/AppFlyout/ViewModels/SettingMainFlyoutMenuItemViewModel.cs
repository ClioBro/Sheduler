using ProjectShedule.AppFlyout.Models;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.Pages.AppFlyout;

namespace ProjectShedule.AppFlyout.ViewModels
{
    public class SettingMainFlyoutMenuItemViewModel : DynamicMainFlyoutMenuItemViewModel
    {
        public SettingMainFlyoutMenuItemViewModel(IThemeController themeController) 
            : base(new MainFlyoutMenuItem(), themeController)
        {
            Title = Lobby.SettingTitle;
            TargetType = typeof(GlobalSetting.SettingsPage);
            DarkImage = "setting_Icon.png";
            LightImage = "setting_Icon_negate.png";
            UpdateDisplayedImage();
        }
    }
}