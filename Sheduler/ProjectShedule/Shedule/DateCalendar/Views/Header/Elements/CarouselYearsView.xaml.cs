using ProjectShedule.Shedule.Calendar.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.Calendar.Views.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselYearsView : ContentView
    {
        #region Property

        public bool IsDragging => carouselYear.IsDragging;

        public static readonly BindableProperty YearsProperty =
          BindableProperty.Create(nameof(Years), typeof(List<YearModel>), typeof(CarouselYearsView), new List<YearModel>(), BindingMode.TwoWay);
        public List<YearModel> Years
        {
            get => (List<YearModel>)GetValue(YearsProperty);
            set => SetValue(YearsProperty, value);
        }


        public static readonly BindableProperty DisplayedYearProperty =
          BindableProperty.Create(nameof(DisplayedYear), typeof(YearModel), typeof(CarouselYearsView), new YearModel(), BindingMode.TwoWay);
        public YearModel DisplayedYear
        {
            get => (YearModel)GetValue(DisplayedYearProperty);
            set => SetValue(DisplayedYearProperty, value);
        }

        public static readonly BindableProperty MaximumYearProperty =
          BindableProperty.Create(nameof(MaximumYear), typeof(int), typeof(CarouselYearsView), 2040, BindingMode.OneWay);
        
        public int MaximumYear
        {
            get => (int)GetValue(MaximumYearProperty);
            set
            {
                if (value >= DateTime.MinValue.Year && value <= DateTime.MaxValue.Year)
                {
                    SetValue(MaximumYearProperty, value);
                }
            }
        }

        public static readonly BindableProperty MinimumYearProperty =
          BindableProperty.Create(nameof(MinimumYear), typeof(int), typeof(CarouselYearsView), 2020, BindingMode.OneWay);
        public int MinimumYear
        {
            get => (int)GetValue(MinimumYearProperty);
            set
            {
                if (value >= DateTime.MinValue.Year && value <= DateTime.MaxValue.Year)
                {
                    SetValue(MinimumYearProperty, value);
                }
            }
        }
        #endregion
        public CarouselYearsView()
        {
            InitializeComponent();
        }
        public void SetCurrentYear(YearModel year)
        {
            carouselYear.CurrentItem = year;
        }
    }
}