using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomStepperView : ContentView
    {
        public CustomStepperView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty MainTextProperty =
           BindableProperty.Create(nameof(MainText), typeof(string), typeof(CustomStepperView), "Empty", BindingMode.TwoWay);
        public string MainText
        {
            get => (string)GetValue(MainTextProperty);
            set => SetValue(MainTextProperty, value);
        }
        public static readonly BindableProperty InfoTextProperty =
           BindableProperty.Create(nameof(InfoText), typeof(string), typeof(CustomStepperView), "NoInfo", BindingMode.TwoWay);
        public string InfoText
        {
            get => (string)GetValue(InfoTextProperty);
            set => SetValue(InfoTextProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
           BindableProperty.Create(nameof(Value), typeof(double), typeof(CustomStepperView), 0.0, BindingMode.TwoWay);
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty IncrementProperty =
           BindableProperty.Create(nameof(Increment), typeof(double), typeof(CustomStepperView), 0.1, BindingMode.TwoWay);
        public double Increment
        {
            get => (double)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }

        public static readonly BindableProperty MaxValueProperty =
           BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(CustomStepperView), 1.0, BindingMode.TwoWay);
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public static readonly BindableProperty MinValueProperty =
           BindableProperty.Create(nameof(MinValue), typeof(double), typeof(CustomStepperView), 0.0, BindingMode.TwoWay);
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        delegate bool MyDelegate(double first, double second);
        private bool MinSravnenie(double first, double second) => first <= second;
        private bool MaxSravnenie(double first, double second) => first >= second;
        private void MinusValue(object sender, EventArgs e)
        {
            Operation(Value - Increment, MinValue, MinSravnenie, minusButton, pluseButton);
        }
        private void PluseValue(object sender, EventArgs e)
        {
            Operation(Value + Increment, MaxValue, MaxSravnenie, pluseButton, minusButton);
        }
        private void Operation(double incoming, double limited, MyDelegate dele, Button buttonBlock, Button open)
        {
            if (dele(incoming, limited))
            {
                incoming = limited;
                buttonBlock.IsEnabled = false;
            }
            open.IsEnabled = true;
            Value = Correct(incoming);
            SetInfo(Value.ToString());
        }
       
        private double Correct(double d)
        {
            decimal deci = (decimal)d;
            return (double)deci;
        }
        private void SetInfo(string text)
        {
            InfoText = text;
        }
    }
}