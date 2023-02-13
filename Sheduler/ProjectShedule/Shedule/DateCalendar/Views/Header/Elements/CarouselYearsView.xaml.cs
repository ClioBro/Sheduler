using ProjectShedule.Shedule.Calendar.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselYearsView : ContentView
    {
        #region BindableProperty

        public static readonly BindableProperty YearsProperty =
          BindableProperty.Create(nameof(Years), typeof(ObservableCollection<YearModel>), typeof(CarouselYearsView), new ObservableCollection<YearModel>(), BindingMode.TwoWay);

        public static readonly BindableProperty DisplayedYearProperty =
          BindableProperty.Create(nameof(DisplayedYear), typeof(YearModel), typeof(CarouselYearsView), new YearModel(), BindingMode.TwoWay);

        #endregion

        public CarouselYearsView()
        {
            InitializeComponent();
        }

        public ObservableCollection<YearModel> Years
        {
            get => (ObservableCollection<YearModel>)GetValue(YearsProperty);
            set => SetValue(YearsProperty, value);
        }
        public YearModel DisplayedYear
        {
            get => (YearModel)GetValue(DisplayedYearProperty);
            set => SetValue(DisplayedYearProperty, value);
        }
        public bool IsDragging => carouselYear.IsDragging;
        public void SetCurrentYear(YearModel year)
        {
            carouselYear.CurrentItem = year;
        }
    }
}