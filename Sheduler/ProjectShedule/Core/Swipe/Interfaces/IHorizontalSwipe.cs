using Xamarin.Forms;

namespace ProjectShedule.Core.Swipe.Interfaces
{
    public interface IHorizontalSwipe
    {
        SwipeItems RightItems { get; }
        SwipeItems LeftItems { get; }
    }
}
