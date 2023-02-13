using ProjectShedule.Core.Enum;
using ProjectShedule.Core.Interfaces;

namespace ProjectShedule.Core.Swipe.Interfaces
{
    public interface ISwipePercentAchievement<T> : IPercentAchievement<T>
    {
        double Threshold { get; }
        double OffSet { get; }
        StatusPosition CurrentPosition { get; }
    }
}
