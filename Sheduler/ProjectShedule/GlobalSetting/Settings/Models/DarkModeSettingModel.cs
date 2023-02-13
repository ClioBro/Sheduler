using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.Models
{
    public class DarkModeSettingModel : SwitchSettingModel
    {
        private readonly IThemeController _themeController;

        public DarkModeSettingModel()
        {
            _themeController = App.ThemeController;
            MainText = SettingResources.DarkModeDopTextLabel;
            Value = _themeController.CurrentTheme is ThemeKey.Dark;
            _themeController.ThemeChanged += OnAppThemeChanged;
        }

        public override bool Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                _themeController.SetThemeOnApp(value ? ThemeKey.Dark : ThemeKey.Light);
            }
        }

        private void OnAppThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Value = e.NewTheme is ThemeKey.Dark;
        }
    }
}
