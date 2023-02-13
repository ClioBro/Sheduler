using ProjectShedule.Core.Swipe;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Swipe
{
    public abstract class BaseNoteSwipeItemView : BaseSwipeItemView
    {
        public BaseNoteSwipeItemView(ImageSource imageSource, string text)
        {
            Image.Source = imageSource;
            Image.Aspect = Aspect.AspectFit;
            Image.HeightRequest = 28;
            Image.WidthRequest = 28;

            Label.Text = text;
            Label.Margin = 3;
            Label.FontAttributes = FontAttributes.Bold;
            Label.TextTransform = TextTransform.Uppercase;
            Label.HorizontalOptions = LayoutOptions.Center;

            StackLayout.Spacing = 4;
            StackLayout.VerticalOptions = LayoutOptions.Center;

            App.ThemeController.ThemeChanged += OnThemeController_ThemeChanged;
        }

        protected abstract void OnThemeController_ThemeChanged(object sender, ThemeChangedEventArgs e);

    }
}
