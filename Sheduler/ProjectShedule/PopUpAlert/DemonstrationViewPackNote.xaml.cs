using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DemonstrationViewPackNote : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static bool isPageOpened = false;
        public DemonstrationViewPackNote(BasePackNoteViewModel packNoteViewModel)
        {
            InitializeComponent();
            packNoteView.BindingContext = packNoteViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            isPageOpened = true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            isPageOpened = false;
        }
    }
}