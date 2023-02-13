using ProjectShedule.Core.Interfaces;
using Xamarin.Forms;

namespace ProjectShedule.Core.Stepper
{
    public class CustomStepperViewModel : FlexLayoutViewModel, IStepper<double>, IStepControll
    {
        private readonly IStepperModel<double> _stepperModel;

        public CustomStepperViewModel() : this(new StepperModelToDouble())
        {

        }
        public CustomStepperViewModel(IStepperModel<double> stepperModel)
        {
            _stepperModel = stepperModel;
            InicializationCommands();
        }

        public double MinValue => _stepperModel.MinValue;
        public double MaxValue => _stepperModel.MaxValue;
        public double Value => _stepperModel.Value;
        public double Increment => _stepperModel.Increment;

        public bool ValueIsVisible { get => GetProperty<bool>(); set => SetProperty(value); }

        public ISimpleButtonViewModel UpButtonViewModel { get; private set; }
        public ISimpleButtonViewModel DownButtonViewModel { get; private set; }

        private void InicializationCommands()
        {
            UpButtonViewModel = new UpValueStepperButtonViewModel()
            {
                Command = new Command(IncrementValueCommandHandler)
            };
            DownButtonViewModel = new DownValueStepperButtonViewModel()
            {
                Command = new Command(DecrementValueCommandHandler)
            };

            if (_stepperModel.CanDecrementValue() == false)
                DownButtonViewModel.SetByDisabled(true);
            if (_stepperModel.CanIncrementValue() == false)
                UpButtonViewModel.SetByDisabled(true);
        }
        private void IncrementValueCommandHandler()
        {
            _stepperModel.SoftIncrementValue();
            NotifyProperty(nameof(Value));

            if (_stepperModel.CanIncrementValue() == false)
                UpButtonViewModel.SetByDisabled(true);
            if (_stepperModel.CanDecrementValue())
                DownButtonViewModel.SetByDisabled(false);
        }
        private void DecrementValueCommandHandler()
        {
            _stepperModel.SoftDecrementValue();
            NotifyProperty(nameof(Value));

            if (_stepperModel.CanDecrementValue() == false)
                DownButtonViewModel.SetByDisabled(true);
            if (_stepperModel.CanIncrementValue())
                UpButtonViewModel.SetByDisabled(false);
        }
    }
}