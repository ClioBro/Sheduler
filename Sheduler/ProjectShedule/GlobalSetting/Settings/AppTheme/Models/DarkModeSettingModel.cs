using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.Models
{
    public class DarkModeSettingModel : SwitchSettingModel
    {
        private readonly IThemeController _themeController;

        public DarkModeSettingModel()
        {
            _themeController = App.ThemeController;
            MainText = SettingResources.DarkModeDopTextLabel;
            Value = _themeController.CurrentTheme is ThemeKey.Dark;
            ValueChanged += OnValueChanged;
            _themeController.ThemeChanged += OnAppThemeChanged;
        }

        private void OnAppThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Value = e.NewTheme is ThemeKey.Dark;
        }

        private void OnValueChanged(object sender, bool value)
        {
            _themeController.SetThemeOnApp(value ? ThemeKey.Dark : ThemeKey.Light);
            NotifyVisualUpdate(this, nameof(Value));
            NotifyVisualUpdate(this, nameof(ValueText));
        }
    }
}
