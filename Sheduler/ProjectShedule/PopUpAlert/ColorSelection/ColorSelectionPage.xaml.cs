using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorSelectionPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static bool IsPageOpened { get; private set; }

        private ColorSelectionViewModel _colorSelectionViewModel;

        public ColorSelectionPage() : this (new ColorSelectionViewModel(new ColorSelectionModel())) { }
        public ColorSelectionPage(ColorSelectionViewModel colorSelectionViewModel)
        {
            InitializeComponent();
            _colorSelectionViewModel = colorSelectionViewModel;
            BindingContext = _colorSelectionViewModel;
        }
        private void PressOnColoredButton(object sender, EventArgs e)
        {
            if (sender is Button button)
                _colorSelectionViewModel.ColorSelectCommand?.Execute(button.BackgroundColor);
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