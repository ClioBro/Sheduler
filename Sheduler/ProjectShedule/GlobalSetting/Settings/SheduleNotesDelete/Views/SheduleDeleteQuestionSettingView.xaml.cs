using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.ViewMdoels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheduleDeleteQuestionSettingView : ContentView
    {
        public SheduleDeleteQuestionSettingView()
        {
            InitializeComponent();
            BindingContext = new SheduleDeleteNotesSettingViewModel();
        }
    }
}