using ProjectShedule.Shedule.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonsSelecterPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static bool IsPageOpened { get; private set; }
        private readonly List<RadioButtonItem> _radioButtons;
        private RadioButtonItem _selectedItem;
        public RadioButtonsSelecterPage(IEnumerable<RadioButtonItem> items, int selectedItemIndex = 0, string title = null)
        {
            InitializeComponent();
            _radioButtons = new List<RadioButtonItem>(items);
            SetSelectedItemByIndex(selectedItemIndex);
            MainText = title;
            BindingContext = this;
        }

        public event EventHandler<RadioButtonItem> SelectedItemChanged;

        public IEnumerable<RadioButtonItem> Items => _radioButtons;
        public RadioButtonItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    SelectedItemChanged?.Invoke(this, value);
                }
            }
        }
        public string MainText { get; }
        public bool MainTextVisible => !string.IsNullOrWhiteSpace(MainText);
        private protected void SetSelectedItemByIndex(int index) => _selectedItem = _radioButtons[index];

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