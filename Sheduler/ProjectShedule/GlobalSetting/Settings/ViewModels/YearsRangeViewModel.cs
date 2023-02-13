using ProjectShedule.Core.Stepper;
using ProjectShedule.GlobalSetting.Base.Models;
using System.ComponentModel;
using ProjectShedule.Shedule.ShapeEvents;
using Xamarin.Forms;
using ProjectShedule.GlobalSetting.Base.ViewModel;
using ProjectShedule.Core;

namespace ProjectShedule.GlobalSetting.Settings.ViewModels
{
    public class YearsRangeViewModel : DateTimeRangeVewModel
    {
        public YearsRangeViewModel(DateTimeRangeModel dateTimeRangeModel)
            : base(dateTimeRangeModel)
        {
            var startMax = dateTimeRangeModel.MaxStart.Year;
            var startMin = dateTimeRangeModel.MinStart.Year;
            var endMax = dateTimeRangeModel.MaxEnd.Year;
            var endMin = dateTimeRangeModel.MinEnd.Year;
            var startValue = dateTimeRangeModel.Value.Start.Year;
            var endValue = dateTimeRangeModel.Value.End.Year;

            CustomStartStepperViewModel = new CustomStepperViewModel(new StepperModelToDouble(startMin, startMax, startValue, increment: 1))
            {
                Direction = FlexDirection.Row,
                AlignContent = FlexAlignContent.Center,
                AlignItems = FlexAlignItems.Center,
                JustifyContent = FlexJustify.SpaceEvenly
            };
            CustomEndStepperViewModel = new CustomStepperViewModel(new StepperModelToDouble(endMin, endMax, endValue, increment: 1))
            {
                Direction = FlexDirection.Row,
                AlignContent = FlexAlignContent.Center,
                AlignItems = FlexAlignItems.Center,
                JustifyContent = FlexJustify.SpaceEvenly
            };

            CustomStartStepperViewModel.PropertyChanged += OnCustomStartStepperViewModel_PropertyChanged;
            CustomEndStepperViewModel.PropertyChanged += OnCustomEndStepperViewModel_PropertyChanged;
        }
        public override CustomStepperViewModel CustomStartStepperViewModel { get; set; }
        public override CustomStepperViewModel CustomEndStepperViewModel { get; set; }

        private void OnCustomStartStepperViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CustomStepperViewModel.Value):
                    SetStartYears((int)CustomStartStepperViewModel.Value);
                    break;
            }
        }
        private void OnCustomEndStepperViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CustomStepperViewModel.Value):
                    SetEndYears((int)CustomEndStepperViewModel.Value);
                    break;
            }
        }

        private void SetStartYears(int year)
        {
            var start = new System.DateTime(year, 1, 1);
            var end = Value.End;
            Value = new DateTimeRange(start, end);
        }
        private void SetEndYears(int year)
        {
            var start = Value.Start;
            var end = new System.DateTime(year, 1, 1);
            Value = new DateTimeRange(start, end);
        }
    }
}