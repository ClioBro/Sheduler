﻿using ProjectShedule.PopUpAlert;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Resources;
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
            EditorPackNoteViewModel editorVM = packNote == null
                ? new EditorPackNoteViewModel()
                : new EditorPackNoteViewModel(GetCleanPackNoteViewModel(packNote));

            editorVM.SavePressed += SavePressedCallBack;

            return new EditorPackNotePage(editorVM);
        }
        private static PackNoteModel GetCleanPackNoteViewModel(IPackNote packNote)
        {
            IEnumerable<IHasSmallTask> hasSmallTasks = packNote.SmallTasks;
            IEnumerable<SmallTaskViewModel> newSmallTasksModels = hasSmallTasks.Select(s => new SmallTaskViewModel(s.SmallTask));
            return new PackNoteModel(packNote.Note, newSmallTasksModels);
        }

        public static async Task<QuestionView.Answer> ShowQuestionForDeletion(this INavigation navigation, string itemNameToBeDeleted)
        {
            return await navigation.ShowPopupAsync(
                new QuestionView(headerText: "Удалить?",
                                 secondaryText: itemNameToBeDeleted,
                                 cancelText: "Отмена",
                                 agreementText: "Да",
                                 sizePopUp: new Size(350, 200)));
        }

        public static async Task ShowAvailableRepeadsAsync(this INavigation navigation, Action<object, RadioButtonItem> selectedItemChangedActionCallBack, RadioButtonItem selectedRadioButton = null)
        {
            if (RadioButtonsSelecterPage.IsPageOpened)
                return;

            var radioButtonsPage = new RadioButtonsSelecterPage(CustomRepeads.RepeadsItems, selectedRadioButton, Repeads.HeaderLabel);
            radioButtonsPage.SelectedItemChanged += (object sender, RadioButtonItem selectedItem) => selectedItemChangedActionCallBack?.Invoke(sender, selectedItem);

            await navigation.PushPopupAsync(radioButtonsPage);
        }
    }
}
