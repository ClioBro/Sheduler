using ProjectShedule.Shedule.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateCalendarView : ContentView
    {
        public const double MaxCarouselLabelSize = 17.0;
        public const double MinCarouseLabelSize = 1.0;

        #region BindingProperties
        public static readonly BindableProperty YearLabelSizeProperty =
          BindableProperty.Create(nameof(YearLabelSize), typeof(double), typeof(DateCalendarView), 16.0, BindingMode.TwoWay);
        public double YearLabelSize
        {
            get => (double)GetValue(YearLabelSizeProperty);
            set => SetValue(YearLabelSizeProperty, value);
        }

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(DateCalendarView), CultureInfo.CurrentCulture, BindingMode.TwoWay);
        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        public static readonly BindableProperty CircleEventsProperty =
            BindableProperty.Create(nameof(CircleEvents), typeof(ReadOnlyObservableCollection<CircleEventModel>), typeof(DateCalendarView), default,  propertyChanged: OnEventsChanged);
        public ReadOnlyObservableCollection<CircleEventModel> CircleEvents
        {
            get => (ReadOnlyObservableCollection<CircleEventModel>)GetValue(CircleEventsProperty);
            set => SetValue(CircleEventsProperty, value);
        }

        public static readonly BindableProperty SelectedDatesProperty =
          BindableProperty.Create(nameof(SelectedDates), typeof(ObservableRangeCollection<DateTime>), typeof(DateCalendarView), new ObservableRangeCollection<DateTime>(), BindingMode.TwoWay);
        public ObservableRangeCollection<DateTime> SelectedDates
        {
            get => (ObservableRangeCollection<DateTime>)GetValue(SelectedDatesProperty);
            set => SetValue(SelectedDatesProperty, value);
        }

        public static readonly BindableProperty MaximunDateTimeProperty =
           BindableProperty.Create(nameof(MaximunDateTime), typeof(DateTime), typeof(DateCalendarView), new DateTime(2040, 12, 12), BindingMode.OneWay);
        public DateTime MaximunDateTime
        {
            get => (DateTime)GetValue(MaximunDateTimeProperty);
            set => SetValue(MaximunDateTimeProperty, value);
        }

        public static readonly BindableProperty MinimumDateTimeProperty =
          BindableProperty.Create(nameof(MinimumDateTime), typeof(DateTime), typeof(DateCalendarView), new DateTime(2020, 12, 12), BindingMode.OneWay);
        public DateTime MinimumDateTime
        {
            get => (DateTime)GetValue(MinimumDateTimeProperty);
            set => SetValue(MinimumDateTimeProperty, value);
        }

        public static readonly BindableProperty DisplayedCarouselDayMontYearProperty =
         BindableProperty.Create(nameof(DisplayedCarouselDayMontYear), typeof(DateTime), typeof(DateCalendarView), DateTime.Today, BindingMode.TwoWay, propertyChanged: OnDisplayedDateChanged);
        public DateTime DisplayedCarouselDayMontYear
        {
            get => (DateTime)GetValue(DisplayedCarouselDayMontYearProperty);
            set => SetValue(DisplayedCarouselDayMontYearProperty, value);
        }

        public static readonly BindableProperty CurrentYearProperty =
          BindableProperty.Create(nameof(CurrentYear), typeof(YearModel), typeof(DateCalendarView), new YearModel(), BindingMode.TwoWay, propertyChanged: OnCarouselDateChanged);
        public YearModel CurrentYear
        {
            get => (YearModel)GetValue(CurrentYearProperty);
            set => SetValue(CurrentYearProperty, value);
        }

        public static readonly BindableProperty CurrentMonthProperty =
         BindableProperty.Create(nameof(CurrentMonth), typeof(MonthModel), typeof(DateCalendarView), new MonthModel(), propertyChanged: OnCarouselDateChanged);
        public MonthModel CurrentMonth
        {
            get => (MonthModel)GetValue(CurrentMonthProperty);
            set => SetValue(CurrentMonthProperty, value);
        }

        public static readonly BindableProperty CurrentDayProperty =
         BindableProperty.Create(nameof(CurrentDay), typeof(DayModel), typeof(DateCalendarView), new DayModel(), propertyChanged: OnCarouselDateChanged);
        public DayModel CurrentDay
        {
            get => (DayModel)GetValue(CurrentDayProperty);
            set => SetValue(CurrentDayProperty, value);
        }

        public static readonly BindableProperty MonthDaysIsVisibleProperty =
         BindableProperty.Create(nameof(MonthDaysIsVisible), typeof(bool), typeof(DateCalendarView), true);
        public bool MonthDaysIsVisible
        {
            get => (bool)GetValue(MonthDaysIsVisibleProperty);
            set => SetValue(MonthDaysIsVisibleProperty, value);
        }

        public static readonly BindableProperty EnableDayCarouselProperty =
         BindableProperty.Create(nameof(EnableDayCarousel), typeof(bool), typeof(DateCalendarView), true);
        public bool EnableDayCarousel
        {
            get => (bool)GetValue(EnableDayCarouselProperty);
            set => SetValue(EnableDayCarouselProperty, value);
        }


        #endregion
        public DateCalendarView()
        {
            InitializeComponent();
            InicializateStartDate(DisplayedCarouselDayMontYear);

            /// Делаю через таймер, так как при быстром Scrolling обновляется каждый CarouselCurrentItem 
            /// что потребляет много ресурсов на обновление DisplayedCarouselDayMontYear

            Device.StartTimer(TimeSpan.FromMilliseconds(500), TimerChecked);
        }
        public List<DayView> DayViews { get; set; } = new List<DayView>();
        public ObservableCollection<DayModel> Days { get; set; } = new ObservableCollection<DayModel>();
        public List<YearModel> Years { get; set; } = new List<YearModel>();
        private void InicializateStartDate(DateTime date)
        {
            InicializateYearsOnCarousel();
            InicializateDaysOnCarousel();
            
            SetCurrentYearOnYearCarousel(date.Year);
            SetCurrentMonthOnMonthCarousel(date.Month);
            SetCurrentDaysOnDaysCarousel(date.Day);
        }
        private void InicializateYearsOnCarousel()
        {
            int year = MinimumDateTime.Year;
            while (year <= MaximunDateTime.Year)
            {
                Years.Add(new YearModel() { Number = year++, LabelSize = YearLabelSize });
            }
        }
        private void InicializateDaysOnCarousel()
        {
            for (int day = 1; day <= DateTime.MaxValue.Day; day++)
            {
                Days.Add(new DayModel() { Date = new DateTime(2021, 10, day), PrimaryTextColor = Color.Red, IsThisMonth = true});
            }
        }


        #region UpdateMonthDaysTimer
        private readonly bool tickedAllTime = true;
        private static bool Scrolled = false;
        private bool TimerChecked()
        {
            if (UpdateDisplayedDateValidation())
            {
                if (CurrentDay.IsThisMonth == false)
                {
                    carouselDayView.SetCurrentDay(Days.LastOrDefault(d => d.IsThisMonth));
                }

                var CurrentDateOnCarousels = TryValidDate();
                if (!Equals(CurrentDateOnCarousels.Date, DisplayedCarouselDayMontYear.Date))
                {
                    DisplayedCarouselDayMontYear = CurrentDateOnCarousels;
                }
            }
            Scrolled = false;
            return tickedAllTime;

            bool UpdateDisplayedDateValidation()
            {
                return Scrolled == false
                    && !carouselDayView.IsDragging
                    && !carouselMonthView.IsDragging
                    && !carouselYearsView.IsDragging;
            }
            DateTime TryValidDate()
            {
                try
                {
                    return new DateTime(CurrentYear.Number, CurrentMonth.Number, CurrentDay.Date.Day);
                }
                catch
                {
                    return new DateTime(CurrentYear.Number, CurrentMonth.Number, DateTime.DaysInMonth(CurrentYear.Number, CurrentMonth.Number));
                }
            }
        }
        #endregion
        public void UpdateCarouselDays()
        {
            int index = 0;
            int maxDayOnMonth = DateTime.MaxValue.Day;

            foreach (DayView dayView in DayViews)
                if (dayView.BindingContext is DayModel dayModel && dayModel.IsThisMonth && index < maxDayOnMonth)
                {
                    var day = Days[index++];
                    day.Date = dayModel.Date;
                    day.IsThisMonth = dayModel.IsThisMonth;
                    day.FirstEvent = dayModel.FirstEvent;
                    day.TwoEvent = dayModel.TwoEvent;
                    day.ThreeEvent = dayModel.ThreeEvent;
                }

            for (; index < maxDayOnMonth; index++)
            {
                var day = Days[index];
                day.Date = new DateTime(2021, 10, index + 1);
                day.IsThisMonth = false;
            }
        }
        public void UpdateCalendarDays()
        {
            monthDaysView.UpdateDays();
        }
        private void SetCurrentYearOnYearCarousel(int year)
        {
            var yearModel = Years.FirstOrDefault(y => y.Number == year);
            carouselYearsView.SetCurrentYear(yearModel);
        }
        private void SetCurrentMonthOnMonthCarousel(int month)
        {
            carouselMonthView.SetCurrentMonth(month);
        }
        private void SetCurrentDaysOnDaysCarousel(int day)
        {
            var dayModel = Days[--day];
            carouselDayView.SetCurrentDay(dayModel);
        }
        #region PropertyChangeds
        
        private static void OnDisplayedDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is DateCalendarView main && newValue is DateTime newDateTime)
            {
                if (newDateTime.Day != main.CurrentDay.Date.Day)
                    main.SetCurrentDaysOnDaysCarousel(newDateTime.Day);

                if (newDateTime.Month != main.CurrentMonth.Number)
                    main.SetCurrentMonthOnMonthCarousel(newDateTime.Month);

                if (newDateTime.Year != main.CurrentYear.Number)
                    main.SetCurrentYearOnYearCarousel(newDateTime.Year);
                
                main.UpdateCarouselDays();
            }
        }
        private static void OnCarouselDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Scrolled = true;
        }
        private static void OnEventsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is DateCalendarView main
                && !Equals(oldValue, newValue)
                && newValue is INotifyCollectionChanged notifyCollectionChanged)
            {
                main.UpdateCarouselDays();
                notifyCollectionChanged.CollectionChanged 
                    += (sender, eventArgs) 
                    => main.UpdateCarouselDays();
            }
        }
        #endregion
    }
}