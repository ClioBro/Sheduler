﻿using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.Models
{
    public class DarkModeSettingModel : SwitchSettingModel
    {
        private readonly ThemeController _themeController;

        public DarkModeSettingModel()
        {
            _themeController = App.ThemeController;
            MainText = SettingResources.DarkModeDopTextLabel;
            Value = _themeController.CurrentTheme is ThemeController.Theme.Dark;
            ValueChanged += OnValueChanged;
            _themeController.ThemeChanged += OnAppThemeChanged;
        }

        private void OnAppThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Value = e.NewTheme is ThemeController.Theme.Dark;
        }

        private void OnValueChanged(object sender, bool value)
        {
            _themeController.SetThemeOnApp(value ? ThemeController.Theme.Dark : ThemeController.Theme.Light);
            NotifyVisualUpdate(this, nameof(Value));
            NotifyVisualUpdate(this, nameof(ValueText));
        }
    }
}
