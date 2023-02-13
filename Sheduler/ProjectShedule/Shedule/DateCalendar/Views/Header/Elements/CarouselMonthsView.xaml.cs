using ProjectShedule.Shedule.Calendar.Models;
using System.Collections.Generic;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselMonthView : ContentView
    {
        #region BindableProperty

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(CarouselMonthView), CultureInfo.CurrentCulture, BindingMode.TwoWay);


        public static readonly BindableProperty DisplayedMonthProperty =
         BindableProperty.Create(nameof(DisplayedMonth), typeof(MonthModel), typeof(CarouselMonthView), new MonthModel(), BindingMode.TwoWay);

        #endregion

        public CarouselMonthView()
        {
            InitializeComponent();
            InicializateMonthsFromCulture();
        }

        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }
        public List<MonthModel> Months { get; private set; } = new List<MonthModel>();
        public MonthModel CurrentMonth { get; set; }
        public MonthModel DisplayedMonth
        {
            get => (MonthModel)GetValue(DisplayedMonthProperty);
            set => SetValue(DisplayedMonthProperty, value);
        }
        public bool IsDragging => carouselMonth.IsDragging;
        public void SetCurrentMonth(int month)
        {
            var monthModel = Months[--month];
            carouselMonth.CurrentItem = monthModel;
        }

        private void InicializateMonthsFromCulture()
        {
            string[] monthNames = Culture.DateTimeFormat.MonthGenitiveNames;
            TextInfo textInfo = Culture.TextInfo;
            int num = 1;
            foreach (string montName in monthNames)
            {
                if (string.IsNullOrWhiteSpace(montName))
                    continue;

                string correctMontName = textInfo.ToTitleCase(montName);

                Months.Add(new MonthModel
                {
                    Name = correctMontName,
                    Number = num++
                });
            }
        }
        private void CarouselMonth_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if (e.CurrentItem is MonthModel newMonth)
            {
                DisplayedMonth = newMonth;
            }
        }
    }
}