using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme
{
    public interface IThemeController : INotifyThemeChanger
    {
        ThemeKey CurrentTheme { get; }
        void SetThemeOnApp(ThemeKey newTheme);
        ResourceDictionary GetCurrentThemeResource();
    }
}
