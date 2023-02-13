using ProjectShedule.Core;
using ProjectShedule.Core.RadioButton;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonsSelecterPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static bool IsPageOpened { get; private set; }

        public RadioButtonsSelecterPage(RadioButtonsSelecterPageViewModel radioButtonsSelecterPageViewModel)
        {
            try
            {
                BindingContext = radioButtonsSelecterPageViewModel;
                InitializeComponent();
            }
            catch (System.Exception e)
            {
                ShowException(e);
            }
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

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
        private async void ShowException(System.Exception exception)
        {
            await Navigation.ShowExceptionViewAsync(exception);
        }
    }

    public class RadioButtonsSelecterPageViewModel : BaseViewModel, IRadioButtonsViewModel
    {
        private RadioButtonItemModel _selectedItem;

        public RadioButtonsSelecterPageViewModel(RadioButtonItemModel[] items, int selectedItemIndex = 0, string title = null, Command<IRadioButtonItem> itemSelectedCommand = null)
        {
            MainText = title;
            Items = items;
            SelectedItem = items[selectedItemIndex];
            SelectedItem.IsChecked = true;
            Subscribe(items);
        }

        public string MainText { get; }
        public bool MainTextVisible => !string.IsNullOrWhiteSpace(MainText);
        public Command<IRadioButtonItem> ItemSelectedCommand { get; set; }

        public RadioButtonItemModel[] Items { get; protected set; }
        public RadioButtonItemModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                    return;
                if (Items.Contains(value) == false)
                    throw new ArgumentException("Value not in collection");
                _selectedItem = value;
                ItemSelectedCommand?.Execute(_selectedItem);
            }
        }
        public string GroupName => "SelecterRadioButtons";

        private void Subscribe(RadioButtonItemModel[] items)
        {
            foreach (RadioButtonItemModel radioButtonItemModel in items)
            {
                radioButtonItemModel.CheckedChanged += OnRadioButtonItemModel_CheckedChanged;
            }
        }
        private void OnRadioButtonItemModel_CheckedChanged(object sender, bool eventArgs)
        {
            if (eventArgs)
            {
                SelectedItem = (RadioButtonItemModel)sender;
            }
        }
    }
}