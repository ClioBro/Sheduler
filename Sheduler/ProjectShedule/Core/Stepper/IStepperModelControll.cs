namespace ProjectShedule.Core.Stepper
{
    public interface IStepperModelControll
    {
        bool CanIncrementValue();
        bool CanDecrementValue();
        void IncrementValue();
        void DecrementValue();
        void SoftIncrementValue();
        void SoftDecrementValue();
    }
}