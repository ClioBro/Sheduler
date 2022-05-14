using ProjectShedule.Shedule.Interfaces;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonsSelecterPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static bool IsPageOpened { get; private set; }
        private readonly List<IRadioButtonItem> _radioButtons;
        private IRadioButtonItem _selectedItem;
        public RadioButtonsSelecterPage(IEnumerable<IRadioButtonItem> items, int selectedItemIndex = 0, string title = null)
        {
            InitializeComponent();
            _radioButtons = new List<IRadioButtonItem>(items);
            SetSelectedItemByIndex(selectedItemIndex);
            MainText = title;
            BindingContext = this;
        }

        public event EventHandler<IRadioButtonItem> SelectedItemChanged;

        public IEnumerable<IRadioButtonItem> Items => _radioButtons;
        public IRadioButtonItem SelectedItem
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