using System;
using static ProjectShedule.GlobalSetting.Settings.AppTheme.IThemeController;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme
{
    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeChangedEventArgs(ThemeKey oldTheme, ThemeKey newTheme)
        {
            NewTheme = newTheme;
            OldTheme = oldTheme;
        }
        public ThemeKey NewTheme { get; }
        public ThemeKey OldTheme { get; }
    }
}
