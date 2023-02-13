using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.OtherElements;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Swipe
{
    public class EditNiteSwipeItemView : BaseNoteSwipeItemView
    {
        public EditNiteSwipeItemView(object commandParameter) : this()
        {
            CommandParameter = commandParameter;
        }
        private EditNiteSwipeItemView()
            : base(((Image)Application.Current.Resources["EditIamge"]).Source, SwipeResource.EditButton)
        {
            BackgroundColor = (Color)Application.Current.Resources["EditBackGroundColor"];
            Label.TextColor = (Color)Application.Current.Resources["EditTextColor"];
        }

        protected override void OnThemeController_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Image.Source = ((Image)Application.Current.Resources["EditIamge"]).Source;
            Label.TextColor = (Color)Application.Current.Resources["EditTextColor"];
            BackgroundColor = (Color)Application.Current.Resources["EditBackGroundColor"];
        }
    }
}
