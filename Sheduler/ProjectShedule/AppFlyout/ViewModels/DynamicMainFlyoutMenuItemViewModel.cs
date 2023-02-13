using ProjectShedule.AppFlyout.Models;
using ProjectShedule.GlobalSetting.Settings.AppTheme;

namespace ProjectShedule.AppFlyout.ViewModels
{
    public class DynamicMainFlyoutMenuItemViewModel : MainFlyoutMenuItemViewModel
    {
        private readonly IThemeController _themeController;
        
        public DynamicMainFlyoutMenuItemViewModel(MainFlyoutMenuItem mainFlyoutMenuItem, IThemeController themeController)
            : base(mainFlyoutMenuItem)
        {
            _themeController = themeController;
            _themeController.ThemeChanged += OnThemeChanged;
        }

        protected void UpdateDisplayedImage()
        {
            SetDisplayedImageByTheme(_themeController.CurrentTheme);
        }

        private void OnThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            SetDisplayedImageByTheme(e.NewTheme);
        }
        private void SetDisplayedImageByTheme(ThemeKey newTheme)
        {
            DisplayedImage = newTheme is ThemeKey.Dark ? LightImage : DarkImage;
        }
    }
}