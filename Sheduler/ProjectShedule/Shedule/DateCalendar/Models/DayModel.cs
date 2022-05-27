using ProjectShedule.Shedule.Calendar.Enums;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Calendar.Models
{
    public interface IHasDate
    {
        DateTime Date { get; }
    }
    public class DayModel : BindableBase<DayModel>, IHasDate
    {
        public DateTime Date
        {
            get => GetProperty<DateTime>();
            set => SetProperty(value)
                    .Notify(nameof(BorderColor));
        }
        public ICommand PressedCommand
        {
            get => GetProperty<ICommand>();
            set => SetProperty(value);
        }
        public ICommand LongPressedCommand
        {
            get => GetProperty<ICommand>();
            set => SetProperty(value);
        }

        public bool IsSelected
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(TextColor),
                            nameof(BorderColor),
                            nameof(BackgroundColor));
        }

        public bool IsThisMonth
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }
        public bool IsToday
            => Date.Date == DateTime.Today;


        #region EventsPropperty
        public bool HasEvents
        {
            get => NumberEvents >= 1;
        }
        public int NumberEvents
        {
            get => GetProperty<int>();
            set => SetProperty(value)
                .Notify(nameof(HasEvents));
        }
        public int MaxColorEvents => 3;
        public bool BackgroundEventIndicator => HasEvents && EventIndicatorType == EventIndicatorType.Background;
        public EventIndicatorType EventIndicatorType
        {
            get => GetProperty(EventIndicatorType.BottomDot);
            set => SetProperty(value)
                    .Notify(// nameof(IsEventDotVisible),
                            nameof(BackgroundEventIndicator),
                            nameof(BackgroundColor));
        }
        public Color DefaultEventColor => Color.FromHex("#3535BB");
        public CircleEventModel FirstEvent
        {
            get => GetProperty<CircleEventModel>();
            set => SetProperty(value);
        }
        public CircleEventModel TwoEvent
        {
            get => GetProperty<CircleEventModel>();
            set => SetProperty(value);
        }
        public CircleEventModel ThreeEvent
        {
            get => GetProperty<CircleEventModel>();
            set => SetProperty(value);
        }
        public Color EventIndicatorColor
        {
            get => GetProperty(DefaultEventColor);
            set => SetProperty(value)
                    .Notify(nameof(EventColor),
                            nameof(BackgroundColor),
                            nameof(BackgroundFullEventColor));
        }
        public Color EventIndicatorSelectedColor
        {
            get => GetProperty(SelectedBackgroundColor);
            set => SetProperty(value)
                    .Notify(nameof(EventColor),
                            nameof(BackgroundColor),
                            nameof(BackgroundFullEventColor));
        }
        public Color EventColor => IsSelected
                                 ? EventIndicatorSelectedColor
                                 : EventIndicatorColor;
        public Color BackgroundFullEventColor => HasEvents && EventIndicatorType == EventIndicatorType.BackgroundFull
                                               ? EventColor
                                               : Color.Default;

        public FlexDirection EventLayoutDirection => HasEvents && EventIndicatorType == EventIndicatorType.TopDot ? FlexDirection.ColumnReverse : FlexDirection.Column;
        #endregion

        public Color DefaultBackGroundColor => (Color)Application.Current.Resources["CalendarDayDefaultBackGroundColor"];
        public Color BackgroundColor
        {
            get
            {
                return IsSelected ? SelectedBackgroundColor : DefaultBackGroundColor;
            }
        }
        public Color DefaultSelectedBackgroundColor => (Color)Application.Current.Resources["SelectedItemBackGround"];
        public Color SelectedBackgroundColor
        {
            get => GetProperty(DefaultSelectedBackgroundColor);
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor));
        }
        public Color TodayBorderColor
        {
            get => GetProperty(Color.FromHex("#FF0000"));
            set => SetProperty(value)
                    .Notify(nameof(BorderColor));
        }
        public Color BorderColor => IsToday
                                   ? TodayBorderColor
                                   : BackgroundColor;
        public Color PrimaryTextColor
        {
            get => GetProperty((Color)Application.Current.Resources["PrimaryTextColor"]);
            set => SetProperty(value)
                .Notify(nameof(TextColor));
        }

        public Color SecondaryTextColor
        {
            get => GetProperty((Color)Application.Current.Resources["SecondaryTextColor"]);
            set => SetProperty(value)
                .Notify(nameof(TextColor));
        }

        public Color TextColor => IsThisMonth ? PrimaryTextColor : SecondaryTextColor;

        public void NotifyColors()
        {
            Notify(nameof(TextColor),
                   nameof(BackgroundColor),
                   nameof(BorderColor));
        }
    }
}
