using System;
using Xamarin.Forms;

namespace ProjectShedule.Core.Swipe.Interfaces
{
    public interface ISwipeItemsControll
    {
#nullable enable annotations
        ISwipeViewController? SwipeViewController { get; }
        void DisableSwipeItem(Type type);
        void EnableSwipeItem(Type type);
        void DisableSwipeItems();
        void EnableSwipeItems();
    }
}
