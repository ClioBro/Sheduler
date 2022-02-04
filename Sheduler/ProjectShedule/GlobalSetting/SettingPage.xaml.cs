using ProjectShedule.GlobalSetting.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            BindingContext = new SettingViewModel() { Title = AppFlyout.Resources.Lobby.SettingTitle };
        }

    }
}