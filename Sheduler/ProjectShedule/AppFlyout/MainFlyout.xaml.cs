using ProjectShedule.AppFlyout.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.AppFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainFlyout : ContentPage
    {
        public ListView ListView;

        public MainFlyout()
        {
            InitializeComponent();

            BindingContext = new MainFlyoutViewModel();
            ListView = MenuItemsListView;
        }
    }
}