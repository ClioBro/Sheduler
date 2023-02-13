using ProjectShedule.PopUpAlert;
using ProjectShedule.Shedule.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.ThrashCan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemovedContentPage : ContentPage
    {
        public RemovedContentPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = new RemovedNotesPageViewModel(Navigation);
            }
            catch(Exception ex)
            {
                ShowException(ex);
            }
        }

        private async void ShowException(Exception exception) => await Navigation.ShowExceptionViewAsync(exception);
    }
}