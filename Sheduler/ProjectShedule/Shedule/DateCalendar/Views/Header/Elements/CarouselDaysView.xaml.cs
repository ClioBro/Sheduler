using ProjectShedule.Shedule.DateCalendar.Models;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselDayView : ContentView
    {
        #region BindableProperty

        public static readonly BindableProperty CurrentDayProperty =
         BindableProperty.Create(nameof(CurrentDay), typeof(DayModel), typeof(CarouselDayView), new DayModel(), BindingMode.TwoWay);

        public static readonly BindableProperty DaysProperty =
         BindableProperty.Create(nameof(Days), typeof(ObservableCollection<DayModel>), typeof(CarouselDayView), new ObservableCollection<DayModel>(), BindingMode.TwoWay);

        #endregion

        public CarouselDayView()
        {
            InitializeComponent();
        }
        public ObservableCollection<DayModel> Days
        {
            get => (ObservableCollection<DayModel>)GetValue(DaysProperty);
            set => SetValue(DaysProperty, value);
        }
        public DayModel CurrentDay
        {
            get => (DayModel)GetValue(CurrentDayProperty);
            set => SetValue(CurrentDayProperty, value);
        }
        public bool IsDragging => carouselDay.IsDragging;

        public void SetCurrentDay(DayModel day)
        {
            carouselDay.CurrentItem = day;
        }
        public void ScrollTo(DayModel day)
        {
            carouselDay.ScrollTo(day);
        }

    }
}