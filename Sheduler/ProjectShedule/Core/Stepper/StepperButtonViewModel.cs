namespace ProjectShedule.Core.Stepper
{
    internal abstract class StepperButtonViewModel : BaseButtonViewModel
    {
        public StepperButtonViewModel()
        {
            IsEnable = true;
            Opacity = 1;
            WidthRequest = 35;
            HeightRequest = 35;
            CornerRadius = 7;
        }
    }
}