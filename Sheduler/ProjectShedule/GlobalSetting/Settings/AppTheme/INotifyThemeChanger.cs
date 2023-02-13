using System;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme
{
    public interface INotifyThemeChanged
    {
        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
    }
}
