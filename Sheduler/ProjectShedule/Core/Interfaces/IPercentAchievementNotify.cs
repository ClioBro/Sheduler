using System;
using ProjectShedule.Core.Swipe;

namespace ProjectShedule.Core.Interfaces
{
    public interface IPercentAchievementNotify<T>
    {
        event EventHandler<SwipePercentAchivementEventArgs<T>> PercentageValueReached;
    }
}
