using ProjectShedule.Language.Resources.PopUp.DeleteQuestion;
using ProjectShedule.PopUpAlert;
using ProjectShedule.PopUpAlert.Question;
using ProjectShedule.Shedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Editor.Models;
using ProjectShedule.Shedule.Editor.ViewModels;
using ProjectShedule.Shedule.PackNotesManager.WorkWithDataBase;
using ProjectShedule.Shedule.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Models
{
    public abstract class IBuilderPackNoteEditorPage : IBuilder<ContentPage>
    {
        protected ContentPage _page;
        public abstract ContentPage Build();
        public abstract IBuilderPackNoteEditorPage SetSavePressedCallBack(Action<ReadOnlyPackNote> SavePressedCallBack);
        public abstract IBuilderPackNoteEditorPage SetDataBaseController(IPackNoteDataBaseController dataBaseController);
        public abstract IBuilderPackNoteEditorPage SetEditTarget(BasePackNoteModel basePackNoteModel);
    }
    public class BuilderPackNoteEditorPage : IBuilderPackNoteEditorPage
    {
        private BasePackNoteModel _basePackNoteModel;
        private IPackNoteDataBaseController _dataBaseController;
        private Action<ReadOnlyPackNote> _savePressedCallBack;

        private EditorPackNoteModel _editorModel;
        private EditorPackNoteViewModel _editorViewModel;
        public override ContentPage Build()
        {
            _editorModel = new EditorPackNoteModel(_basePackNoteModel, _dataBaseController);
            SetEvent();
            _editorViewModel = new EditorPackNoteViewModel(_editorModel);
            _page = new EditorPackNotePage(_editorViewModel);
            return _page;
        }
        public override IBuilderPackNoteEditorPage SetEditTarget(BasePackNoteModel basePackNoteModel)
        {
            _basePackNoteModel = basePackNoteModel;
            return this;
        }
        public override IBuilderPackNoteEditorPage SetSavePressedCallBack(Action<ReadOnlyPackNote> savePressedCallBack)
        {
            _savePressedCallBack = savePressedCallBack;
            return this;
        }
        public override IBuilderPackNoteEditorPage SetDataBaseController(IPackNoteDataBaseController dataBaseController)
        {
            _dataBaseController = dataBaseController;
            return this;
        }
        
        private void SetEvent()
        {
            _editorModel.PackNoteSaved += (object sender, ReadOnlyPackNote readOnlyPackNote)
               => _savePressedCallBack.Invoke(readOnlyPackNote);
        }
    }
    public static class CreaterPageManager
    {
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
