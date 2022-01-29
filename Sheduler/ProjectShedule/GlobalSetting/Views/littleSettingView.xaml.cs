
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class littleSettingView : ContentView
    {
        public littleSettingView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty MainTextProperty =
          BindableProperty.Create(nameof(MainText), typeof(string), typeof(littleSettingView), "no info", BindingMode.TwoWay);
        public string MainText
        {
            get => (string)GetValue(MainTextProperty);
            set => SetValue(MainTextProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
          BindableProperty.Create(nameof(Value), typeof(double), typeof(littleSettingView), 0.0, BindingMode.TwoWay);
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        public static readonly BindableProperty MinValueProperty =
          BindableProperty.Create(nameof(MinValue), typeof(double), typeof(littleSettingView), 0.0, BindingMode.TwoWay);
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }
        public static readonly BindableProperty MaxValueProperty =
          BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(littleSettingView), 10.0, BindingMode.TwoWay);
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }
    }
}