using ProjectShedule.AppFlyout.Models;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.Pages.AppFlyout;

namespace ProjectShedule.AppFlyout.ViewModels
{
    public class RecycleBinMainFlyoutMenuItemViewModel : DynamicMainFlyoutMenuItemViewModel
    {
        public RecycleBinMainFlyoutMenuItemViewModel(IThemeController themeController)
            : base(new MainFlyoutMenuItem(), themeController)
        {
            Title = Lobby.RecycleBinTitle;
            TargetType = typeof(ThrashCan.RemovedContentPage);
            DarkImage = "trash_icon.png";
            LightImage = "trash_icon_negate.png";
            UpdateDisplayedImage();
        }
    }
}