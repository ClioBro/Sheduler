namespace ProjectShedule.Core.Search
{
    public class SimpleSearchBarAnimation : ISearchBarAnimation
    {
        public SimpleSearchBarAnimation(Xamarin.Forms.Animation searchBarLeftAnimation, Xamarin.Forms.Animation searchBarRightAnimation)
        {
            FocusedAnimation = searchBarLeftAnimation;
            UnfocusedAnimation = searchBarRightAnimation;
        }

        public Xamarin.Forms.Animation FocusedAnimation { get; set; }
        public Xamarin.Forms.Animation UnfocusedAnimation { get; set; }
    }
}