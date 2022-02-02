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
        private RadioButtonItem _selectedItem;
        public RadioButtonsSelecterPage(IEnumerable<RadioButtonItem> items, RadioButtonItem selectedItem = null, string title = null)
        {
            InitializeComponent();
            Items = items;
            _selectedItem = selectedItem;
            MainText = title;
            BindingContext = this;
        }

        public event EventHandler<RadioButtonItem> SelectedItemChanged;

        public IEnumerable<RadioButtonItem> Items { get; }
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