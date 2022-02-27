using ProjectShedule.Shedule.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentView
    {
        #region BindingProperties
        public static readonly BindableProperty YearLabelSizeProperty =
          BindableProperty.Create(nameof(YearLabelSize), typeof(double), typeof(Main), 16.0, BindingMode.TwoWay);
        public double YearLabelSize
        {
            get => (double)GetValue(YearLabelSizeProperty);
            set => SetValue(YearLabelSizeProperty, value);
        }

        public const double MaxCarouselLabelSize = 17.0;
        public const double MinCarouseLabelSize = 1.0;

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(Main), CultureInfo.CurrentCulture, BindingMode.TwoWay);
        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        public static readonly BindableProperty DayEventsProperty =
            BindableProperty.Create(nameof(DayEvents), typeof(IEnumerable<ICircleEvent>), typeof(Main), null, BindingMode.TwoWay, propertyChanged: OnEventsChanged);
        public IEnumerable<ICircleEvent> DayEvents
        {
            get => (IEnumerable<ICircleEvent>)GetValue(DayEventsProperty);
            set => SetValue(DayEventsProperty, value);
        }

        public static readonly BindableProperty SelectedDatesProperty =
          BindableProperty.Create(nameof(SelectedDates), typeof(List<DateTime>), typeof(Main), new List<DateTime>(), BindingMode.TwoWay);
        
        public List<DateTime> SelectedDates
        {
            get => (List<DateTime>)GetValue(SelectedDatesProperty);
            set => SetValue(SelectedDatesProperty, value);
        }

        public static readonly BindableProperty MaximunDateTimeProperty =
           BindableProperty.Create(nameof(MaximunDateTime), typeof(DateTime), typeof(Main), new DateTime(2040, 12, 12), BindingMode.OneWay);
        public DateTime MaximunDateTime
        {
            get => (DateTime)GetValue(MaximunDateTimeProperty);
            set => SetValue(MaximunDateTimeProperty, value);
        }

        public static readonly BindableProperty MinimumDateTimeProperty =
          BindableProperty.Create(nameof(MinimumDateTime), typeof(DateTime), typeof(Main), new DateTime(2020, 12, 12), BindingMode.OneWay);
        public DateTime MinimumDateTime
        {
            get => (DateTime)GetValue(MinimumDateTimeProperty);
            set => SetValue(MinimumDateTimeProperty, value);
        }

        public static readonly BindableProperty DisplayedCalendarMonthYearProperty =
          BindableProperty.Create(nameof(DisplayedCalendarMonthYear), typeof(DateTime), typeof(Main), DateTime.Today, BindingMode.TwoWay, propertyChanged: OnMonthYearChanged);
        public DateTime DisplayedCalendarMonthYear
        {
            get => (DateTime)GetValue(DisplayedCalendarMonthYearProperty);
            set => SetValue(DisplayedCalendarMonthYearProperty, value);
        }

        public static readonly BindableProperty DisplayedCarouselDayMontYearProperty =
         BindableProperty.Create(nameof(DisplayedCarouselDayMontYear), typeof(DateTime), typeof(Main), DateTime.Today, BindingMode.TwoWay, propertyChanged: OnCarouselDayMontYearChanged);
        public DateTime DisplayedCarouselDayMontYear
        {
            get => (DateTime)GetValue(DisplayedCarouselDayMontYearProperty);
            set => SetValue(DisplayedCarouselDayMontYearProperty, value);
        }

        public static readonly BindableProperty CurrentYearProperty =
          BindableProperty.Create(nameof(CurrentYear), typeof(YearModel), typeof(Main), new YearModel(), BindingMode.OneWay, propertyChanged: OnCurrentYearChanged);
        public YearModel CurrentYear
        {
            get => (YearModel)GetValue(CurrentYearProperty);
            set => SetValue(CurrentYearProperty, value);
        }

        public static readonly BindableProperty CurrentMonthProperty =
         BindableProperty.Create(nameof(CurrentMonth), typeof(MonthModel), typeof(Main), new MonthModel(), propertyChanged: OnCurrentMonthChanged);
        public MonthModel CurrentMonth
        {
            get => (MonthModel)GetValue(CurrentMonthProperty);
            set 
            {
                SetValue(CurrentMonthProperty, value);
            } 
        }

        public static readonly BindableProperty CurrentDayProperty =
         BindableProperty.Create(nameof(CurrentDay), typeof(DayModel), typeof(Main), new DayModel(), propertyChanged: OnCurrentDayChanged);
        public DayModel CurrentDay
        {
            get => (DayModel)GetValue(CurrentDayProperty);
            set => SetValue(CurrentDayProperty, value);
        }

        public static readonly BindableProperty MonthDaysIsVisibleProperty =
         BindableProperty.Create(nameof(MonthDaysIsVisible), typeof(bool), typeof(Main), true);
        public bool MonthDaysIsVisible
        {
            get => (bool)GetValue(MonthDaysIsVisibleProperty);
            set => SetValue(MonthDaysIsVisibleProperty, value);
        }
        #endregion
        public Main()
        {
            InitializeComponent();
            InicializateStartDate(DateTime.Today);

            /// Делаю через таймер, так как при быстром Scrolling обновляется каждый CarouselCurrentItem, 
            /// что потребляет много ресурсов на обновление DisplayedMonthYear
            Device.StartTimer(TimeSpan.FromMilliseconds(400), TimerChecked);
        }

        public List<DayView> DayViews { get; set; } = new List<DayView>();
        public ObservableCollection<DayModel> Days { get; set; } = new ObservableCollection<DayModel>();
        public List<YearModel> Years { get; set; } = new List<YearModel>();
        private void InicializateStartDate(DateTime date)
        {
            DisplayedCalendarMonthYear = date;
            DisplayedCarouselDayMontYear = date;
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
                Years.Add(new YearModel() { Number = year++, YearLabelSize = YearLabelSize });
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
            if (Scrolled == false && !carouselDayView.IsDragging && !carouselMonthView.IsDragging && !carouselYearsView.IsDragging)
            {
                if (CurrentDay.IsThisMonth == false && !carouselDayView.IsDragging)
                {
                    carouselDayView.SetCurrentDay(Days.LastOrDefault(d => d.IsThisMonth));
                }

                if (CurrentYear.Number != DisplayedCalendarMonthYear.Year
                    || CurrentMonth.Number != DisplayedCalendarMonthYear.Month)
                {
                    DisplayedCalendarMonthYear = new DateTime(CurrentYear.Number, CurrentMonth.Number, 1);
                }

                if (CurrentDay.IsThisMonth == false)
                {
                    CurrentDay = Days.LastOrDefault(d => d.IsThisMonth);
                }

                var CurrentDateOnCarousels = new DateTime(CurrentYear.Number, CurrentMonth.Number, CurrentDay.Date.Day);
                if (!Equals(CurrentDateOnCarousels.Date, DisplayedCarouselDayMontYear.Date))
                {
                    DisplayedCarouselDayMontYear = CurrentDateOnCarousels;
                    if (DisplayedCarouselDayMontYear.Date != SelectedDates.FirstOrDefault())
                    {
                        SelectedDates = new List<DateTime>() { DisplayedCarouselDayMontYear.Date };
                    }
                }
            }
            Scrolled = false;
            return tickedAllTime;
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
        private static void OnCurrentYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Scrolled = true;
        }
        private static void OnCurrentMonthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Scrolled = true;
        }
        private static void OnCurrentDayChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Scrolled = true;
        }

        private static void OnCarouselDayMontYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Main main && newValue is DateTime newDateTime)
            {
                if (newDateTime.Day != main.CurrentDay.Date.Day) 
                    main.SetCurrentDaysOnDaysCarousel(newDateTime.Day);

                if (newDateTime.Month != main.CurrentMonth.Number)
                    main.SetCurrentMonthOnMonthCarousel(newDateTime.Month);

                if(newDateTime.Year != main.CurrentYear.Number)
                    main.SetCurrentYearOnYearCarousel(newDateTime.Year);
            }
        }

        private static void OnEventsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Main main && !Equals(oldValue, newValue))
            {
                main.UpdateCarouselDays();
            }
        }

        private static void OnMonthYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Main main && newValue is DateTime newDateTime)
            {
                if (main.CurrentMonth.Number != newDateTime.Month)
                    main.CurrentMonth.Number = newDateTime.Month;

                if (main.CurrentYear.Number != newDateTime.Year)
                    main.CurrentYear.Number = newDateTime.Year;

                main.UpdateCarouselDays();
            }
        }
        #endregion
    }
}