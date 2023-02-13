using ProjectShedule.Core.Swipe.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DemonstrationViewPackNote : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static bool isPageOpened = false;
        public DemonstrationViewPackNote(NoteViewModel packNoteViewModel)
        {
            InitializeComponent();
            ISwipeItemsControll swipeItemsControll = packNoteViewModel;
            swipeItemsControll.DisableSwipeItems();
            packNoteViewModel.SmallTasksContainerIsEnable = false;
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