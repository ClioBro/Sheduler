using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Shedule.Calendar.Controls.SelectionEngine;
using ProjectShedule.Shedule.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthDays : ContentView
    {
        public enum SelectionMode
        {
            Single, Multiply
        }
        public enum DaysTitleMaxLength
        {
            OneChar = 1,
            TwoChars = 2,
            ThreeChars = 3
        }

        internal BaseSelectionHasDateEngine<DayModel> _selectionDateEngine;
        internal INotifyThemeChange _notifyThemeChanged = App.ThemeController;
        public MonthDays()
        {
            InitializeComponent();
            BuildSelectionDaysEngine(SelectionDatesMode);
            PressedCommand = new Command<DayModel>(SelectInEngine);
            LongPressedCommand = new Command<DayModel>(PropagateUpLongPressed);
            _selectionDateEngine.SelectedDatesTime = SelectedDates; 
            InitializeDays();
        }
        ~MonthDays() => DiposeDayViews();
        private void DiposeDayViews()
        {
            foreach (var dayView in daysControl.Children.OfType<DayView>())
            {
                dayView.BindingContext = null;
            }
        }

        #region Simple Properties

        public ICommand PressedCommand { get; private set; }
        public ICommand LongPressedCommand { get; private set; }

        #endregion

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

        public static readonly BindableProperty DayLongPressedCommandProperty =
            BindableProperty.Create(nameof(DayLongPressedCommand), typeof(ICommand), typeof(MonthDays), null, BindingMode.TwoWay);
        public ICommand DayLongPressedCommand
        {
            get => (ICommand)GetValue(DayLongPressedCommandProperty);
            set => SetValue(DayLongPressedCommandProperty, value);
        }

        public static readonly BindableProperty CircleEventsProperty =
            BindableProperty.Create(nameof(CircleEvents), typeof(ReadOnlyObservableCollection<CircleEventModel>), typeof(MonthDays), default, BindingMode.TwoWay, propertyChanged: OnCircleEventsChanged);
        public ReadOnlyObservableCollection<CircleEventModel> CircleEvents
        {
            get => (ReadOnlyObservableCollection<CircleEventModel>)GetValue(CircleEventsProperty);
            set => SetValue(CircleEventsProperty, value);
        }

        public static readonly BindableProperty SelectionDatesModeProperty =
            BindableProperty.Create(nameof(SelectionDatesMode), typeof(SelectionMode), typeof(MonthDays), SelectionMode.Single, propertyChanging:OnSelectedDatesModeChanged);
        public SelectionMode SelectionDatesMode
        {
            get => (SelectionMode)GetValue(SelectionDatesModeProperty);
            set => SetValue(SelectionDatesModeProperty, value);
        }

        public static readonly BindableProperty SelectedDatesProperty =
          BindableProperty.Create(nameof(SelectedDates), typeof(ObservableRangeCollection<DateTime>), typeof(MonthDays), new ObservableRangeCollection<DateTime>(), BindingMode.TwoWay, propertyChanged: OnSelectedDatesChanged);
        public ObservableRangeCollection<DateTime> SelectedDates
        {
            get => (ObservableRangeCollection<DateTime>)GetValue(SelectedDatesProperty);
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
        private static void OnCircleEventsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MonthDays monthDays
                && newValue is INotifyCollectionChanged notifyCollectionChanged)
            {
                monthDays.UpdateDays();
                notifyCollectionChanged.CollectionChanged += (sender, eventArgs) => monthDays.UpdateEventsOnDays();
            }
        }
        private static void OnSelectedDatesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MonthDays monthDays
                && newValue is ObservableRangeCollection<DateTime> newObservableCollection)
            {
                monthDays._selectionDateEngine.SelectedDatesTime = newObservableCollection;
            }
        }
        private static void OnSelectedDatesModeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MonthDays monthDays
                && newValue is SelectionMode selectingMode)
            {
                var tempSelectedCollection = monthDays._selectionDateEngine.SelectedDatesTime;
                monthDays.BuildSelectionDaysEngine(selectingMode);
                tempSelectedCollection.Clear();
                monthDays._selectionDateEngine.SelectedDatesTime = tempSelectedCollection;
            }
        }

        #endregion

        private void BuildSelectionDaysEngine(SelectionMode selectingMode)
        {
            switch (selectingMode)
            {
                case SelectionMode.Single:
                    _selectionDateEngine = new SingleSelectionDayEngine();
                    break;
                case SelectionMode.Multiply:
                    _selectionDateEngine = new MultipleSelectionDayEngine();
                    break;
            }
        }
        private void SelectInEngine(DayModel dayModel) => _selectionDateEngine.SelectItem(dayModel);
        private void PropagateUpLongPressed(DayModel dayModel)
        {
            RequestVibration();
            DateTime dateTime = dayModel.Date;
            DayLongPressedCommand?.Execute(dateTime);
        }
        private void RequestVibration()
        {
            try
            {
                TimeSpan duration = TimeSpan.FromSeconds(0.1);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException)
            {
                // Feature not supported on device
            }
            catch (Exception)
            {
                // Other error has occurred.
            }
        }
        private void InitializeDays()
        {
            var newDaysViews = new List<DayView>();
            foreach (DayView dayView in daysControl.Children.OfType<DayView>())
            {
                var dayModel = new DayModel();

                dayView.BindingContext = dayModel;
                dayModel.PressedCommand = PressedCommand;
                dayModel.LongPressedCommand = LongPressedCommand;
                _notifyThemeChanged.ThemeChanged += dayModel.OnAppThemeChanged;
                newDaysViews.Add(dayView);
            }
            DayViews = newDaysViews;
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

                    AssigmentEvents(dayModel, events);
                }
            });
        }

        private void UpdateEventsOnDays()
        {
            foreach (var dayView in DayViews)
            {
                var dayModel = dayView.BindingContext as DayModel;

                if (CircleEvents != null)
                {
                    var thisDayEvents = CircleEvents.Where(d => d.DateTime.Date == dayModel.Date).ToArray();
                    AssigmentEvents(dayModel, thisDayEvents);
                }
            }
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