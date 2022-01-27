using ProjectShedule.Calendar.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Calendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderView : ContentView
    {
        public HeaderView()
        {
            InitializeComponent();
        }
        #region BindableProperty
        public static readonly BindableProperty DisplayedYearProperty =
             BindableProperty.Create(nameof(DisplayedYear), typeof(YearModel), typeof(HeaderView), new YearModel());
        public YearModel DisplayedYear
        {
            get => (YearModel)GetValue(DisplayedYearProperty);
            set => SetValue(DisplayedYearProperty, value);
        }
        #endregion
    }
}