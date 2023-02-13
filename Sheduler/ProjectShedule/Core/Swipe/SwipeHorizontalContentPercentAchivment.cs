using System;
using ProjectShedule.Core.Swipe.Interfaces;

namespace ProjectShedule.Core.Swipe
{
    public class SwipePercentAchivementEventArgs<T> : EventArgs
    {
        public SwipePercentAchivementEventArgs(ISwipePercentAchievement<T> swipePercentAchievement)
        {
            SwipePercentAchievement = swipePercentAchievement;
        }
        public ISwipePercentAchievement<T> SwipePercentAchievement { get; }
    }
}
