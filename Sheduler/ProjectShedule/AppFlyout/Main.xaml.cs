using ProjectShedule.AppFlyout.ViewModels;
using ProjectShedule.AppFlyout.Views;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.AppFlyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : Xamarin.Forms.FlyoutPage
    {
        public Main()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MainFlyoutMenuItemViewModel item)
            {
                Page page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;

                FlyoutPage.ListView.SelectedItem = null;
            }
        }
    }
}