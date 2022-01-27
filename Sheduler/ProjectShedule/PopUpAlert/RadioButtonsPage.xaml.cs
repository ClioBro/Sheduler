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
        private Repead _selectedRepead;
        public RadioButtonsSelecterPage(IEnumerable<Repead> repeads, Repead selectedRepead = null, string title = null)
        {
            InitializeComponent();
            Repeads = repeads;
            _selectedRepead = selectedRepead;
            MainText = title;
            BindingContext = this;
        }

        public event EventHandler<Repead> SelectedItemChanged;

        public IEnumerable<Repead> Repeads { get; }
        public Repead SelectedRepead
        {
            get => _selectedRepead;
            set
            {
                if (_selectedRepead != value)
                {
                    _selectedRepead = value;
                    SelectedItemChanged?.Invoke(this, value);
                }
            }
        }
        public string MainText { get; }
        public bool MainTextVisible => string.IsNullOrWhiteSpace(MainText);

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