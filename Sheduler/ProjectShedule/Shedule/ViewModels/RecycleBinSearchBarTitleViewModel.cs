using ProjectShedule.Core;
using ProjectShedule.Core.Search;
using System.Windows.Input;

namespace ProjectShedule.Shedule.ViewModels
{
    internal class RecycleBinSearchBarTitleViewModel : SearchBarTitleViewModel, ISearchBarAnimationControll
    {
        private readonly ISearchBarAnimation _searchBarAnimation;
        public RecycleBinSearchBarTitleViewModel()
        {
            Title = Language.Resources.Pages.AppFlyout.Lobby.RecycleBinTitle;
            Placeholder = Language.Resources.OtherElements.SearchBar.Placeholder;
            WidthRequest = 50;
            _searchBarAnimation = new SimpleSearchBarAnimation(
                new Xamarin.Forms.Animation(SetSearchBarWidth, 50, 250),
                new Xamarin.Forms.Animation(SetSearchBarWidth, 250, 50)
                );
        }

        public override ISearchBarAnimation SearchBarAnimation => _searchBarAnimation;
        public override ICommand SerchCommand { get; set; }

        private void SetSearchBarWidth(double value)
        {
            WidthRequest = value;
        }
    }
}