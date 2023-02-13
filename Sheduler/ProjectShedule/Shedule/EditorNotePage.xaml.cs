using ProjectShedule.Shedule.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Editor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorNotePage : ContentPage
    {
        public EditorNotePage(EditorNotePageViewModel editorViewModel)
        {
            InitializeComponent();
            editorViewModel.Navigation = this.Navigation;
            BindingContext = editorViewModel;
        }

        public static bool IsPageOpened { get; private set; }

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