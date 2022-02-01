using ProjectShedule.GlobalSetting.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Views
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