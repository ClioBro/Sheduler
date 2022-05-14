using ProjectShedule.Language.Resources.PopUp.DeleteQuestion;
using ProjectShedule.PopUpAlert;
using ProjectShedule.PopUpAlert.Question;
using ProjectShedule.Shedule.Editor.Models;
using ProjectShedule.Shedule.Editor.ViewModels;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Models
{
    public static class CreaterPageManager
    {
        public static EditorPackNotePage CreateEditorPage(Action SavePressedCallBack, IPackNote packNote = null)
        {
            EditorPackNoteModel editorModel = packNote == null
                ? new EditorPackNoteModel()
                : new EditorPackNoteModel(GetCleanPackNoteViewModel(packNote));

            EditorPackNoteVM editorVM = new EditorPackNoteVM(editorModel);

            editorVM.SavedActionCallBack += SavePressedCallBack;

            return new EditorPackNotePage(editorVM);
        }
        private static PackNoteModel GetCleanPackNoteViewModel(IPackNote packNote)
        {
            IEnumerable<IHasSmallTask> hasSmallTasks = packNote.SmallTasks;
            IEnumerable<SmallTaskViewModel> newSmallTasksModels = hasSmallTasks.Select(s => new SmallTaskViewModel(s.SmallTask));
            return new PackNoteModel(packNote.Note, newSmallTasksModels);
        }

        public static async Task<QuestionView.Answer> ShowQuestionForDeletion(this INavigation navigation, string itemNameToBeDeleted, string dopText = null)
        {
            return await navigation.ShowPopupAsync(
                new QuestionView(headerText: QuestionResource.HeaderLabel,
                                 secondaryText: itemNameToBeDeleted,
                                 dopText: dopText,
                                 cancelText: QuestionResource.CancelButtonText,
                                 agreementText: QuestionResource.DeleteButtonText,
                                 sizePopUp: new Size(300, 200)));;
        }

        public static async Task ShowAvailableRepeadsAsync(this INavigation navigation, RadioButtonsSelecterPage radioButtonsSelecterPage)
        {
            await navigation.PushPopupAsync(radioButtonsSelecterPage);
        }
    }
}
