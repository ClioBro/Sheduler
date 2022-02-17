using ProjectShedule.Shedule.Editor.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorPackNotePage : ContentPage
    {
        public EditorPackNotePage() : this(new EditorPackNoteVM()) { }

        public EditorPackNotePage(EditorPackNoteVM editorViewModel)
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