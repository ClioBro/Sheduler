using ProjectShedule.Language.Resources.PopUp.DeleteQuestion;
using ProjectShedule.PopUpAlert.ColorSelection;
using ProjectShedule.PopUpAlert.Exception;
using ProjectShedule.PopUpAlert.Question;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert
{
    public static class PopUpShowExtensions
    {
        public static async Task<QuestionView.Answer> ShowQuestionForDeletionAsync(this INavigation navigation, string itemNameToBeDeleted, string dopText = null)
        {
            return await navigation.ShowPopupAsync(
                new QuestionView(headerText: QuestionResource.HeaderLabel,
                                 secondaryText: itemNameToBeDeleted,
                                 dopText: dopText,
                                 cancelText: QuestionResource.CancelButtonText,
                                 agreementText: QuestionResource.DeleteButtonText,
                                 sizePopUp: new Size(300, 200)));
        }

        public static async Task ShowAvailableRepeatsAsync(this INavigation navigation, RadioButtonsSelecterPage radioButtonsSelecterPage)
        {
            await navigation.PushPopupAsync(radioButtonsSelecterPage);
        }
        public static async Task ShowColorSelection(this INavigation Navigation, NoteColorSelectionPage colorSelectionPage)
        {
            await Navigation.PushPopupAsync(colorSelectionPage);
        }
        public static async Task ShowExceptionViewAsync(this INavigation navigation, System.Exception exception)
        {
            await navigation.ShowPopupAsync(new ExceptionView(exception));
        }
    }

}
