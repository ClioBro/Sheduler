using System;
using ProjectShedule.Core.Search;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchTitleView : ContentView
    {
        private ISearchBarAnimation _searchBarAnimation;

        public SearchTitleView()
        {
            BindingContextChanged += OnSearchTitleView_BindingContextChanged;
            InitializeComponent();
        }

        protected SearchBar SearchBar => searchBar;

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            SearchBar.Focus();
        }

        private void SearchBar_Focused(object sender, FocusEventArgs e)
        {
            if (_searchBarAnimation == null)
                return;
            ExecuteAnimation(_searchBarAnimation.FocusedAnimation);
        }
        private void SearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            if (_searchBarAnimation == null)
                return;
            ExecuteAnimation(_searchBarAnimation.UnfocusedAnimation);
        }

        private void ExecuteAnimation(Xamarin.Forms.Animation animation)
        {
            animation.Commit(SearchBar, "SearchBarAnimation", rate: 16, 300);
        }
        private void OnSearchTitleView_BindingContextChanged(object sender, EventArgs e)
        {
            if (BindingContext is ISearchBarAnimationControll searchBarAnimationControll) 
            {
                _searchBarAnimation = searchBarAnimationControll.SearchBarAnimation;
            }
        }
    }
}