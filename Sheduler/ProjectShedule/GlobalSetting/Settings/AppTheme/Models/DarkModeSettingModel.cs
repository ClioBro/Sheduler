using ProjectShedule.GlobalSetting.Models;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.Models
{
    public class DarkModeSettingModel : SwitchsSettingModel
    {
        private ThemeController _themeController;
        public DarkModeSettingModel()
            : base(falseText: Resources.SettingResources.FalseLabel,
                  trueText: Resources.SettingResources.TrueLabel)
        {
            _themeController = App.ThemeController;
            MainText = Resources.SettingResources.DarkModeDopTextLabel;
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
            OnPropertyChanged(this, nameof(StatusText));
        }
    }
}
