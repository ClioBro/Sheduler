using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DaysOfWeek : ContentView
    {
        public enum DaysTitleMaxLength
        {
            OneChar = 1,
            TwoChars = 2,
            ThreeChars = 3
        }
        public DaysOfWeek ()
        {
            InitializeComponent();
            UpdateDayTitles();
        }

        #region BindableProperties
        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(DaysOfWeek), CultureInfo.CurrentCulture);
        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        public static readonly BindableProperty DaysTitleMaximumLengthProperty =
          BindableProperty.Create(nameof(DaysTitleMaximumLength), typeof(DaysTitleMaxLength), typeof(DaysOfWeek), DaysTitleMaxLength.TwoChars);
        public DaysTitleMaxLength DaysTitleMaximumLength
        {
            get => (DaysTitleMaxLength)GetValue(DaysTitleMaximumLengthProperty);
            set => SetValue(DaysTitleMaximumLengthProperty, value);
        }
        #endregion

        private void UpdateDayTitles()
        {
            int dayNumber = (int)Culture.DateTimeFormat.FirstDayOfWeek;

            foreach (var dayLabel in daysControl.Children.OfType<Label>())
            {
                string abberivatedDayName = Culture.DateTimeFormat.AbbreviatedDayNames[dayNumber];
                dayLabel.Text = abberivatedDayName.ToUpper().Substring(0, (int)DaysTitleMaximumLength > abberivatedDayName.Length ? abberivatedDayName.Length : (int)DaysTitleMaximumLength);
                dayNumber = (dayNumber + 1) % 7;
            }
        }
    }
}