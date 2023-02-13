namespace ProjectShedule.Core.Search
{
    public interface ISearchBarAnimation
    {
        Xamarin.Forms.Animation FocusedAnimation { get; set; }
        Xamarin.Forms.Animation UnfocusedAnimation { get; set; }
    }
}