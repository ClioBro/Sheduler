using ProjectShedule.PopUpAlert;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            try
            {
                BindingContext = new SettingPageViewModel();
                InitializeComponent();
            }
            catch (Exception e)
            {
                ShowExceptionView(e);
            }
            finally { BindingContext = null; }
        }

        private async void ShowExceptionView(Exception e)
        {
            await Navigation.ShowExceptionViewAsync(e);
        } 
    }
}