using ProjectShedule.Shedule.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonsView : ContentView
    {
        public RadioButtonsView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public static readonly BindableProperty RadioButtonItemsProperty =
          BindableProperty.Create(nameof(RadioButtonItems), typeof(IEnumerable<RadioButtonItem>), typeof(RadioButtonsView), null);
        public IEnumerable<RadioButtonItem> RadioButtonItems
        {
            get => (IEnumerable<RadioButtonItem>)GetValue(RadioButtonItemsProperty);
            set => SetValue(RadioButtonItemsProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty =
          BindableProperty.Create(nameof(SelectedItem), typeof(RadioButtonItem), typeof(RadioButtonsView), new RadioButtonItem());
        public RadioButtonItem SelectedItem
        {
            get => (RadioButtonItem)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly BindableProperty OrientationProperty =
         BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(RadioButtonsView), StackOrientation.Horizontal);
        public StackOrientation Orientation
        {
            get => (StackOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }
    }
}