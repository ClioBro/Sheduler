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
            // Create Image For This
            Image.Source = imageSource;
            Image.Aspect = Aspect.AspectFit;
            Image.HeightRequest = 17;
            Image.WidthRequest = 17;
            // Create Label For This
            Label.Text = text;
            Label.Margin = 3;
            Label.FontAttributes = FontAttributes.Bold;
            Label.TextTransform = TextTransform.Uppercase;
            Label.HorizontalOptions = LayoutOptions.Center;
            // Create Stack For This
            StackLayout.Spacing = 2;
            StackLayout.VerticalOptions = LayoutOptions.Center;
            StackLayout.HorizontalOptions = LayoutOptions.End;

            _backGroundColorSwich = new BackGroundColorSwich(StackLayout);

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
