using ProjectShedule.GlobalSetting.Settings.SheduleEvents.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheduleEventsSettingView : ContentView
    {
        public SheduleEventsSettingView()
        {
            InitializeComponent();
            BindingContext = new SheduleEventsSettingViewModel();
        }
    }
}