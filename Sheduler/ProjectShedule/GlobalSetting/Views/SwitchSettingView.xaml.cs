
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.GlobalSetting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwitchSettingView : ContentView
    {
        public SwitchSettingView()
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
          BindableProperty.Create(nameof(Value), typeof(bool), typeof(SettingPushButtonView), false, BindingMode.TwoWay);
        public bool Value
        {
            get => (bool)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}