using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.OtherElements;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Swipe
{
    public class DeleteNoteSwipeItemView : BaseNoteSwipeItemView
    {
        public DeleteNoteSwipeItemView(object commandParameter) : this()
        {
            CommandParameter = commandParameter;
        }
        private DeleteNoteSwipeItemView()
            : base(((Image)Application.Current.Resources["DeleteIamge"]).Source, SwipeResource.DeleteButton)
        {
            BackgroundColor = (Color)Application.Current.Resources["DeleteBackGroundColor"];
            Label.TextColor = (Color)Application.Current.Resources["DeleteTextColor"];
        }

        protected override void OnThemeController_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Image.Source = ((Image)Application.Current.Resources["DeleteIamge"]).Source;
            Label.TextColor = (Color)Application.Current.Resources["DeleteTextColor"];
            BackgroundColor = (Color)Application.Current.Resources["DeleteBackGroundColor"];
        }
    }
}
