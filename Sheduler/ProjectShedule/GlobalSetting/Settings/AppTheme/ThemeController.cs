using ProjectShedule.Calendar.Controls.ThemingDemo;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme
{
    public class ThemeController : Setting<ThemeController>, IThemeController
    {
        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;
        private protected ICollection<ResourceDictionary> _mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        private readonly Dictionary<ThemeKey, ResourceDictionary> _themesDictionaries;
        public ThemeController()
        {
            _themesDictionaries = new Dictionary<ThemeKey, ResourceDictionary>
            {
                {ThemeKey.Dark, new DarkTheme()},
                {ThemeKey.Light, new LightTheme()}
            };
            CurrentTheme = GetPreference(nameof(ThemeKey.Dark), false) ? ThemeKey.Dark : ThemeKey.Light;
        }
        public ThemeKey CurrentTheme { get; protected set; }
        public void SetThemeOnApp(ThemeKey newTheme)
        {
            if (CurrentTheme != newTheme)
                SetCurrentTheme(newTheme);
        }
        public ResourceDictionary GetCurrentThemeResource()
        {
            return _themesDictionaries[CurrentTheme];
        }

        private void SetCurrentTheme(ThemeKey newTheme)
        {
            if (_themesDictionaries.ContainsKey(newTheme))
            {
                ThemeKey oldTheme = CurrentTheme;
                CurrentTheme = newTheme;
                SetResourceOnApp();
                ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(oldTheme, newTheme));
                SaveThemeOnAppMemory();
            }
            else
                throw new Exception($"ERROR: Key {newTheme} not found in _themesDictionaries");
        }
        private void SetResourceOnApp()
        {
            _mergedDictionaries.Clear();
            _mergedDictionaries.Add(_themesDictionaries[CurrentTheme]);
        }

        private void SaveThemeOnAppMemory()
        {
            SavePreference(nameof(ThemeKey.Dark), CurrentTheme is ThemeKey.Dark);
        }
    }
}
