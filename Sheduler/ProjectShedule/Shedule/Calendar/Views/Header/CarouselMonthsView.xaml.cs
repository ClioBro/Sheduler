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
        #region Property
        public MonthModel CurrentMonth { get; set; }
        public List<MonthModel> Months { get; set; } = new List<MonthModel>();
        public bool IsDragging => carouselMonth.IsDragging;

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(CarouselMonthView), CultureInfo.CurrentCulture, BindingMode.TwoWay);
        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }


        public static readonly BindableProperty DisplayedMonthProperty =
         BindableProperty.Create(nameof(DisplayedMonth), typeof(MonthModel), typeof(CarouselMonthView), new MonthModel(), BindingMode.TwoWay);
        public MonthModel DisplayedMonth
        {
            get => (MonthModel)GetValue(DisplayedMonthProperty);
            set 
            {
                SetValue(DisplayedMonthProperty, value);
            }
        }

        #endregion
        public CarouselMonthView()
        {
            InitializeComponent();
            InicializateMonths();
        }
        private void InicializateMonths()
        {
            string[] monthNames = Culture.DateTimeFormat.MonthGenitiveNames;
            int num = 1;
            foreach (string name in monthNames)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    TextInfo textInfo = Culture.TextInfo;
                    Months.Add(new MonthModel { Name = textInfo.ToTitleCase(name), Number = num++ });
                }
            }
        }
       
        public void SetCurrentMonth(int month)
        {
            var monthModel = Months[--month];
            carouselMonth.CurrentItem = monthModel;
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