using ProjectShedule.PopUpAlert.ColorSelection;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert
{
    public static class ShowControll
    {
        public static async Task ShowColorSelection(this INavigation Navigation, ColorSelectionPage colorSelectionPage)
        {
            await Navigation.PushPopupAsync(colorSelectionPage);
        }
    }
}
