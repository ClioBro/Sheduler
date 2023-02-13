using ProjectShedule.Core.Swipe;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.OtherElements;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Swipe
{
    public class DeleteSmallTaskSwipeItemView : BaseSmallTaskSwipeItemView
    {
        public DeleteSmallTaskSwipeItemView(object commandParameter) : this()
        {
            CommandParameter = commandParameter;
        }
        private DeleteSmallTaskSwipeItemView()
            : base(((Image)Application.Current.Resources["DeleteIamge"]).Source, SwipeResource.DeleteButton)
        {
            BackgroundColor = (Color)Application.Current.Resources["DeleteBackGroundColor"];
            Label.TextColor = (Color)Application.Current.Resources["DeleteTextColor"];

            BackGroundColorSwich.In = BackgroundColor;
            BackGroundColorSwich.On = Color.Silver;
        }

        protected override void OnThemeController_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Image.Source = ((Image)Application.Current.Resources["DeleteIamge"]).Source;
            Label.TextColor = (Color)Application.Current.Resources["DeleteTextColor"];
            BackgroundColor = (Color)Application.Current.Resources["DeleteBackGroundColor"];
        }
    }
}
