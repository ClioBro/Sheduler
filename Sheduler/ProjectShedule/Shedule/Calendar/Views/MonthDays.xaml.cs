using ProjectShedule.Shedule.Calendar.Controls.SelectionEngine;
using ProjectShedule.Shedule.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthDays : ContentView
    {
        public enum DaysTitleMaxLength
        {
            OneChar = 1,
            TwoChars = 2,
            ThreeChars = 3
        }

        internal SelectedDateControll _selectedDateControll = new SelectedDateControll();
        
        public void AssignNewSelectedDay(DayModel newSelectedDay)
        {
            _selectedDateControll.SelectDay(newSelectedDay);

            if (newSelectedDay.IsSelected)
                SelectedDates = new List<DateTime>() { newSelectedDay.Date };
            else
                SelectedDates = new List<DateTime>();
        }
        public MonthDays()
        {
            InitializeComponent();
            DayTappedCommand = new Command<DayModel>(AssignNewSelectedDay);
            InitializeDays();
        }
        ~MonthDays() => DiposeDayViews();


        #region Bindable properties
        public static readonly BindableProperty DaysTitleMaximumLengthProperty =
          BindableProperty.Create(nameof(DaysTitleMaximumLength), typeof(DaysTitleMaxLength), typeof(MonthDays), DaysTitleMaxLength.TwoChars);
        public DaysTitleMaxLength DaysTitleMaximumLength
        {
            get => (DaysTitleMaxLength)GetValue(DaysTitleMaximumLengthProperty);
            set => SetValue(DaysTitleMaximumLengthProperty, value);
        }

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(MonthDays), CultureInfo.CurrentCulture);
        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }


        public static readonly BindableProperty DisplayedMonthYearProperty =
          BindableProperty.Create(nameof(DisplayedMonthYear), typeof(DateTime), typeof(MonthDays), DateTime.Today, propertyChanged: DisplayedMonthYearChanged);
        public DateTime DisplayedMonthYear
        {
            get => (DateTime)GetValue(DisplayedMonthYearProperty);
            set => SetValue(DisplayedMonthYearProperty, value);
        }


        public static readonly BindableProperty DayTappedCommandProperty =
            BindableProperty.Create(nameof(DayTappedCommand), typeof(ICommand), typeof(MonthDays), null);
        public ICommand DayTappedCommand
        {
            get => (ICommand)GetValue(DayTappedCommandProperty);
            set => SetValue(DayTappedCommandProperty, value);
        }


        public static readonly BindableProperty DayEventsProperty =
            BindableProperty.Create(nameof(DayEvents), typeof(IEnumerable<ICircleEvent>), typeof(MonthDays), Enumerable.Empty<ICircleEvent>(), BindingMode.TwoWay, propertyChanged: DayEventsChanged);
        
        public IEnumerable<ICircleEvent> DayEvents
        {
            get => (IEnumerable<ICircleEvent>)GetValue(DayEventsProperty);
            set => SetValue(DayEventsProperty, value);
        }


        public static readonly BindableProperty SelectedDatesProperty =
          BindableProperty.Create(nameof(SelectedDates), typeof(List<DateTime>), typeof(MonthDays), new List<DateTime>(), BindingMode.TwoWay);
        
        public List<DateTime> SelectedDates
        {
            get => (List<DateTime>)GetValue(SelectedDatesProperty);
            set => SetValue(SelectedDatesProperty, value);
        }

        public static readonly BindableProperty DayViewsProperty =
          BindableProperty.Create(nameof(DayViews), typeof(List<DayView>), typeof(MonthDays), new List<DayView>(), BindingMode.OneWayToSource);

        public List<DayView> DayViews
        {
            get => (List<DayView>)GetValue(DayViewsProperty);
            set => SetValue(DayViewsProperty, value);
        }

        #endregion

        #region PropertyChanged
        private static void DayEventsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MonthDays monthDays && !Equals(newValue, oldValue) && newValue != null)
            {
                monthDays.UpdateDays();
            }
        }
        private static void DisplayedMonthYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MonthDays monthDays && !Equals(oldValue, newValue) && newValue is DateTime )
            {
                monthDays.UpdateDays();
            }
        }

      
        #endregion

        private void InitializeDays()
        {
            var newDaysViews = new List<DayView>();
            foreach (DayView dayView in daysControl.Children.OfType<DayView>())
            {
                var dayModel = new DayModel();

                dayView.BindingContext = dayModel;
                dayModel.TappedCommand = DayTappedCommand;
                App.ThemeController.ThemeChanged += dayModel.OnAppThemeChanged;
                newDaysViews.Add(dayView);
            }
            DayViews = newDaysViews;
        }

        private void DiposeDayViews()
        {
            foreach (var dayView in daysControl.Children.OfType<DayView>())
            {
                dayView.BindingContext = null;
            }
        }
        public void UpdateDays()
        {
            var monthStart = new DateTime(DisplayedMonthYear.Year, DisplayedMonthYear.Month, 1);
            var addDays = ((int)Culture.DateTimeFormat.FirstDayOfWeek) - (int)monthStart.DayOfWeek;

            if (addDays > 0)
                addDays -= 7;

            foreach (var dayView in DayViews)
            {
                var currentDate = monthStart.AddDays(addDays++);
                var dayModel = dayView.BindingContext as DayModel;

                var events = DayEvents.Where(d => d.DateTime.Date == currentDate.Date);

                dayModel.Date = currentDate.Date;
                dayModel.IsThisMonth = currentDate.Month == DisplayedMonthYear.Month;
                dayModel.IsSelected = _selectedDateControll.IsDateSelected(dayModel.Date);

                AssigmentEvents(dayModel, events);
            }
        }
        public async void UpdateDaysAsync()
        {
            await Task.Run(() =>
            {
                var monthStart = new DateTime(DisplayedMonthYear.Year, DisplayedMonthYear.Month, 1);
                var addDays = ((int)Culture.DateTimeFormat.FirstDayOfWeek) - (int)monthStart.DayOfWeek;

                if (addDays > 0)
                    addDays -= 7;

                foreach (var dayView in DayViews)
                {
                    var currentDate = monthStart.AddDays(addDays++);
                    var dayModel = dayView.BindingContext as DayModel;

                    var events = DayEvents.Where(d => d.DateTime.Date == currentDate.Date).AsEnumerable();

                    dayModel.Date = currentDate.Date;
                    dayModel.IsThisMonth = currentDate.Month == DisplayedMonthYear.Month;
                    dayModel.TappedCommand = DayTappedCommand;

                    AssigmentEvents(dayModel, events);
                }
            });
        }

        private void AssigmentEvents(DayModel dayModel, IEnumerable<ICircleEvent> events)
        {
            dayModel.NumberEvents = events.Count();
            ICircleEvent[] arrEvents = events.ToArray();

            dayModel.FirstEvent = (CircleEventModel)(arrEvents.Length >= 1 ? arrEvents?[0] : new CircleEventModel());
            dayModel.TwoEvent = (CircleEventModel)(arrEvents.Length >= 2 ? arrEvents?[1] : new CircleEventModel());
            dayModel.ThreeEvent = (CircleEventModel)(arrEvents.Length >= 3 ? arrEvents?[2] : new CircleEventModel());
        }
    }
}