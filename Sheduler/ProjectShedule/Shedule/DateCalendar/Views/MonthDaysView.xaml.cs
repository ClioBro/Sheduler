using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Shedule.Calendar.Controls.SelectionEngine;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.DateCalendar.Controls.SelectionEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
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

        internal BaseSelectionDateEngine<DayModel> _selectionDateEngine = new MultipleSelectionDayEngine();
        internal INotifyThemeChange _notifyThemeChanged = App.ThemeController;
        public MonthDays()
        {
            InitializeComponent();
            DayTappedCommand = new Command<DayModel>(_selectionDateEngine.SelectItem);

            INotifyCollectionChanged notifyCollectionChanged = _selectionDateEngine.SelectedDatesTime;
            notifyCollectionChanged.CollectionChanged += OnSelectionEngineCollectionChanged;

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
          BindableProperty.Create(nameof(DisplayedMonthYear), typeof(DateTime), typeof(MonthDays), DateTime.Today, BindingMode.TwoWay, propertyChanged: OnDisplayedMonthYearChanged);
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


        public static readonly BindableProperty CircleEventsProperty =
            BindableProperty.Create(nameof(CircleEvents), typeof(ReadOnlyObservableCollection<CircleEventModel>), typeof(MonthDays), default, BindingMode.TwoWay, propertyChanged: OnDayEventsChanged);
        
        public ReadOnlyObservableCollection<CircleEventModel> CircleEvents
        {
            get => (ReadOnlyObservableCollection<CircleEventModel>)GetValue(CircleEventsProperty);
            set => SetValue(CircleEventsProperty, value);
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
        private static void OnDisplayedMonthYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MonthDays monthDays
                && !Equals(newValue, oldValue))
            {
                monthDays.UpdateDays();
            }
        }
        private static void OnDayEventsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MonthDays monthDays
                && !Equals(newValue, oldValue)
                && newValue is INotifyCollectionChanged notifyCollectionChanged)
            {
                monthDays.UpdateDays();
                notifyCollectionChanged.CollectionChanged += (sender, eventArgs) => monthDays.UpdateDays();
            }
        }
        private void OnSelectionEngineCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SelectedDates = _selectionDateEngine.SelectedDatesTime.ToList();
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
                _notifyThemeChanged.ThemeChanged += dayModel.OnAppThemeChanged;
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
            var displayedDateTime = DisplayedMonthYear;
            var monthStart = new DateTime(displayedDateTime.Year, displayedDateTime.Month, 1);
            var addDays = ((int)Culture.DateTimeFormat.FirstDayOfWeek) - (int)monthStart.DayOfWeek;

            if (addDays > 0)
                addDays -= 7;

            foreach (var dayView in DayViews)
            {
                var currentDate = monthStart.AddDays(addDays++);
                var dayModel = dayView.BindingContext as DayModel;

                dayModel.Date = currentDate.Date;
                dayModel.IsThisMonth = currentDate.Month == displayedDateTime.Month;
                dayModel.IsSelected = _selectionDateEngine.DateIsSelected(dayModel.Date);

                if (CircleEvents != null)
                {
                    var events = CircleEvents.Where(d => d.DateTime.Date == currentDate.Date).ToArray();
                    AssigmentEvents(dayModel, events);
                }
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

                    var events = CircleEvents.Where(d => d.DateTime.Date == currentDate.Date).ToArray();

                    dayModel.Date = currentDate.Date;
                    dayModel.IsThisMonth = currentDate.Month == DisplayedMonthYear.Month;
                    dayModel.TappedCommand = DayTappedCommand;

                    AssigmentEvents(dayModel, events);
                }
            });
        }

        private void AssigmentEvents(DayModel dayModel, CircleEventModel[] events)
        {
            dayModel.NumberEvents = events.Length;

            dayModel.FirstEvent = events.Length >= 1 ? events?[0] : new CircleEventModel();
            dayModel.TwoEvent = events.Length >= 2 ? events?[1] : new CircleEventModel();
            dayModel.ThreeEvent = events.Length >= 3 ? events?[2] : new CircleEventModel();
        }
    }
}