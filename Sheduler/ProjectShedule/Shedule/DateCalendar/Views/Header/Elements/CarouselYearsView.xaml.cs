using ProjectShedule.Shedule.Calendar.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselYearsView : ContentView
    {
        #region Property

        public bool IsDragging => carouselYear.IsDragging;

        public static readonly BindableProperty YearsProperty =
          BindableProperty.Create(nameof(Years), typeof(ObservableCollection<YearModel>), typeof(CarouselYearsView), new ObservableCollection<YearModel>(), BindingMode.TwoWay);
        public ObservableCollection<YearModel> Years
        {
            get => (ObservableCollection<YearModel>)GetValue(YearsProperty);
            set => SetValue(YearsProperty, value);
        }

        public static readonly BindableProperty DisplayedYearProperty =
          BindableProperty.Create(nameof(DisplayedYear), typeof(YearModel), typeof(CarouselYearsView), new YearModel(), BindingMode.TwoWay);
        public YearModel DisplayedYear
        {
            get => (YearModel)GetValue(DisplayedYearProperty);
            set => SetValue(DisplayedYearProperty, value);
        }
        #endregion
        public CarouselYearsView()
        {
            InitializeComponent();
        }
        public void SetCurrentYear(YearModel year)
        {
            carouselYear.CurrentItem = year;
        }
    }
}