using ProjectShedule.GlobalSetting.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheduleDeleteQuestionSettingView : ContentView
    {
        public SheduleDeleteQuestionSettingView()
        {
            InitializeComponent();
            BindingContext = new SheduleDeleteQuestionSettingViewModel();
        }
    }
}