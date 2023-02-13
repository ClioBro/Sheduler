using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme
{
    public interface IThemeController : INotifyThemeChanged
    {
        ThemeKey CurrentTheme { get; }
        void SetThemeOnApp(ThemeKey newTheme);
        ResourceDictionary GetCurrentThemeResource();
    }
}
