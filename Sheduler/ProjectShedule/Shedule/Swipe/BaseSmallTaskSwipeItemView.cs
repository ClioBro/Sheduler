using ProjectShedule.Core.Interfaces;
using ProjectShedule.Core.Swipe;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Swipe
{
    public abstract class BaseSmallTaskSwipeItemView : BaseSwipeItemView, ISwichBackGroundColor
    {
        private readonly BackGroundColorSwich _backGroundColorSwich;
        
        public BaseSmallTaskSwipeItemView(ImageSource imageSource, string text)
            : base()
        {
            Image.Source = imageSource;
            Image.Aspect = Aspect.AspectFit;
            Image.HeightRequest = 20;
            Image.WidthRequest = 20;

            Label.Text = text;
            Label.FontSize = 12;

            Label.TextTransform = TextTransform.Uppercase;
            Label.HorizontalOptions = LayoutOptions.Center;

            StackLayout.Spacing = 0;
            StackLayout.VerticalOptions = LayoutOptions.Center;
            StackLayout.HorizontalOptions = LayoutOptions.End;

            _backGroundColorSwich = new BackGroundColorSwich(this);

            App.ThemeController.ThemeChanged += OnThemeController_ThemeChanged;
        }

        protected BackGroundColorSwich BackGroundColorSwich => _backGroundColorSwich;

        public void SwichBackGroundColor()
        {
            BackGroundColorSwich.SwichColor();
        }

        protected abstract void OnThemeController_ThemeChanged(object sender, ThemeChangedEventArgs e);
    }
}
