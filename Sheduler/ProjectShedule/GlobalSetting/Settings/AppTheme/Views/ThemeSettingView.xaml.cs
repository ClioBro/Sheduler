using ProjectShedule.GlobalSetting.Settings.AppTheme.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Settings.AppTheme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeSettingView : ContentView
    {
        public ThemeSettingView()
        {
            InitializeComponent();
            BindingContext = new SheduleThemeSettingViewModel();
        }
    }
}