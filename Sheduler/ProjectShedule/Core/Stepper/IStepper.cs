namespace ProjectShedule.Core.Stepper
{
    public interface IStepper<T>
    {
        T MinValue { get; }
        T MaxValue { get; }
        T Value { get; }
        T Increment { get; }
    }
}