using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert
{
    public static class ShowControll
    {
        public static async void ShowColorSelection(this INavigation Navigation, ColorSelectionPage colorSelectionPage)
        {
            await Navigation.PushPopupAsync(colorSelectionPage);
        }
    }
}
