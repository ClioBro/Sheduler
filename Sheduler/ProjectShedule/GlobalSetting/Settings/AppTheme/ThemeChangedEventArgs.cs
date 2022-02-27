using System;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme
{
    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeChangedEventArgs(ThemeController.Theme oldTheme, ThemeController.Theme newTheme)
        {
            NewTheme = newTheme;
            OldTheme = oldTheme;
        }
        public ThemeController.Theme NewTheme { get; }
        public ThemeController.Theme OldTheme { get; }
    }
}
