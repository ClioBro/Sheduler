using ProjectShedule.Shedule.Calendar.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : ContentView
    {
        public DayView()
        {
            InitializeComponent();
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (BindingContext is DayModel dayModel && dayModel.IsThisMonth)
            {
                dayModel.PressedCommand?.Execute(dayModel);
            }
        }
    }
}