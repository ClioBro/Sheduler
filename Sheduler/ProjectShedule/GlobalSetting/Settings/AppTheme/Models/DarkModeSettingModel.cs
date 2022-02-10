using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Resource;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.Models
{
    public class DarkModeSettingModel : SwitchsSettingModel
    {
        private ThemeController _themeController;
        public DarkModeSettingModel()
            : base(falseText: SettingResources.FalseLabel,
                  trueText: SettingResources.TrueLabel)
        {
            _themeController = App.ThemeController;
            MainText = SettingResources.DarkModeDopTextLabel;
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
