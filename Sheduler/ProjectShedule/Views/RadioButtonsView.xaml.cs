using ProjectShedule.Shedule.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonsView : ContentView
    {
        public RadioButtonsView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemsProperty =
          BindableProperty.Create(nameof(Items), typeof(RadioButtonItem[]), typeof(RadioButtonsView), Array.Empty<RadioButtonItem>());
        public RadioButtonItem[] Items
        {
            get => (RadioButtonItem[])GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty =
          BindableProperty.Create(nameof(SelectedItem), typeof(RadioButtonItem), typeof(RadioButtonsView), null, BindingMode.TwoWay);
        public RadioButtonItem SelectedItem
        {
            get => (RadioButtonItem)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly BindableProperty GroupNameProperty =
         BindableProperty.Create(nameof(GroupName), typeof(string), typeof(RadioButtonsView), null, BindingMode.TwoWay);
        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }
    }
}