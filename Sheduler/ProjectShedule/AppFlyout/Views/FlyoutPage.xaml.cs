using ProjectShedule.AppFlyout.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.AppFlyout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPage : ContentPage
    {
        public ListView ListView { get; set; }

        public FlyoutPage()
        {
            InitializeComponent();

            BindingContext = new MainFlyoutViewModel();
            ListView = MenuItemsListView;
        }
    }
}