using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.OtherElements;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Swipe
{
    public class ReveiveSmallTaskSwipeItemView : BaseSmallTaskSwipeItemView
    {
        public ReveiveSmallTaskSwipeItemView(object commandParameter) : this()
        {
            CommandParameter = commandParameter;
        }
        private ReveiveSmallTaskSwipeItemView()
            : base(((Image)Application.Current.Resources["ReviveIamge"]).Source, SwipeResource.ReviveButton)
        {
            BackgroundColor = (Color)Application.Current.Resources["ReviveBackGroundColor"];
            Label.TextColor = (Color)Application.Current.Resources["ReviveTextColor"];
        }

        protected override void OnThemeController_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Image.Source = ((Image)Application.Current.Resources["ReviveIamge"]).Source;
            Label.TextColor = (Color)Application.Current.Resources["ReviveTextColor"];
            BackgroundColor = (Color)Application.Current.Resources["ReviveBackGroundColor"];
        }
    }
}
