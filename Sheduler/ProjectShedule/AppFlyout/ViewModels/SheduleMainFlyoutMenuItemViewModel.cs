using ProjectShedule.AppFlyout.Models;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.Pages.AppFlyout;

namespace ProjectShedule.AppFlyout.ViewModels
{
    public class SheduleMainFlyoutMenuItemViewModel : DynamicMainFlyoutMenuItemViewModel
    {
        public SheduleMainFlyoutMenuItemViewModel(IThemeController themeController)
            : base(new MainFlyoutMenuItem(), themeController)
        {
            Title = Lobby.SheduleTitle;
            TargetType = typeof(Shedule.ShedulePage);
            DarkImage = "note_icon.png";
            LightImage = "note_icon_negate.png";
            UpdateDisplayedImage();
        }
    }
}