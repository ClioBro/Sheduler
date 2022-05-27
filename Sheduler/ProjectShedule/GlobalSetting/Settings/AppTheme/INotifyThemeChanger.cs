using System;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme
{
    public interface INotifyThemeChanger
    {
        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
    }
}
