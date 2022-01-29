
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPushButtonView : ContentView
    {
        public SettingPushButtonView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty MainTextProperty =
          BindableProperty.Create(nameof(MainText), typeof(string), typeof(SettingPushButtonView), "no info", BindingMode.TwoWay);
        public string MainText
        {
            get => (string)GetValue(MainTextProperty);
            set => SetValue(MainTextProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
          BindableProperty.Create(nameof(Value), typeof(double), typeof(SettingPushButtonView), 0.0, BindingMode.TwoWay);
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        public static readonly BindableProperty MinValueProperty =
          BindableProperty.Create(nameof(MinValue), typeof(double), typeof(SettingPushButtonView), 0.0, BindingMode.TwoWay);
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }
        public static readonly BindableProperty MaxValueProperty =
          BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(SettingPushButtonView), 10.0, BindingMode.TwoWay);
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }
    }
}