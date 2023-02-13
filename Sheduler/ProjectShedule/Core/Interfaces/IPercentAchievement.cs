namespace ProjectShedule.Core.Interfaces
{
    public interface IPercentAchievement<T> : IPercent<T>
    {
        float MaxValue { get; }
        float MinValue { get; }
        float Achivement { get; }
    }
}
