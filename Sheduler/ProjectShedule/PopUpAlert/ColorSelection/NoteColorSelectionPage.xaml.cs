using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteColorSelectionPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static bool IsPageOpened { get; private set; }
        public NoteColorSelectionPage(NoteColorSelectionPageViewModel concretePageViewModel)
        {
            BindingContext = concretePageViewModel;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            IsPageOpened = true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IsPageOpened = false;
        }
    }
}