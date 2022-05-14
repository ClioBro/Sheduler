using ProjectShedule.Calendar.Controls.ThemingDemo;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme
{
    public class ThemeController : Setting<ThemeController>
    {
        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;
        private protected ICollection<ResourceDictionary> _mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        private readonly Dictionary<Theme, ResourceDictionary> _themesDictionaries;
        public ThemeController()
        {
            _themesDictionaries = new Dictionary<Theme, ResourceDictionary>
            {
                {Theme.Dark, new DarkTheme()},
                {Theme.Light, new LightTheme()}
            };
            CurrentTheme = GetPreference(nameof(Theme.Dark), false) ? Theme.Dark : Theme.Light;
        }
        public Theme CurrentTheme { get; private set; }
        public void SetThemeOnApp(Theme newTheme)
        {
            if (CurrentTheme != newTheme) 
                SetCurrentTheme(newTheme);
        }
        public ResourceDictionary GetCurrentThemeResource()
        {
            return _themesDictionaries[CurrentTheme];
        }
        private void SetCurrentTheme(Theme newTheme)
        {
            if (_themesDictionaries.ContainsKey(newTheme))
            {
                Theme oldTheme = CurrentTheme;
                CurrentTheme = newTheme;
                SetResourceOnApp();
                ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(oldTheme, newTheme));
                SaveThemeOnApp();
            }
            else
                throw new Exception($"ERROR: Key {newTheme} not found in _themesDictionaries");
        }
        private void SetResourceOnApp()
        {
            _mergedDictionaries.Clear();
            _mergedDictionaries.Add(_themesDictionaries[CurrentTheme]);
        }

        private void SaveThemeOnApp()
        {
            SavePreference(nameof(Theme.Dark), CurrentTheme is Theme.Dark);
        }

        public enum Theme
        {
            Dark, Light, Nier
        }
    }
}
