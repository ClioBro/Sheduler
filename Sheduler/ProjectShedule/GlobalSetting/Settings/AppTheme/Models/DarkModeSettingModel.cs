using ProjectShedule.GlobalSetting.Models;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.Models
{
    public class DarkModeSettingModel : SwitchsSettingModel
    {
        private ThemeController _themeController;
        public DarkModeSettingModel(ThemeController themeController)
        {
            _themeController = themeController;
            MainText = "DarkMode:";
            Status = _themeController.CurrentTheme is ThemeController.Theme.Dark;
            StatusChanged += OnStatusChanged;
            _themeController.ThemeChanged += OnAppThemeChanged;
        }

        private void OnAppThemeChanged(ThemeController.Theme oldTheme, ThemeController.Theme newTheme)
        {
            Status = newTheme is ThemeController.Theme.Dark;
        }

        private void OnStatusChanged(object sender, bool value)
        {
            _themeController.SetThemeOnApp(value ? ThemeController.Theme.Dark : ThemeController.Theme.Light);
            OnPropertyChanged(this, nameof(Status));
        }
    }
}
