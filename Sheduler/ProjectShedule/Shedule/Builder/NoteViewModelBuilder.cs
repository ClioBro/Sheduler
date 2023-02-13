
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Builder.Base;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using System.Windows.Input;


namespace ProjectShedule.Shedule.Builder
{
    public class NoteViewModelBuilder : BaseDeletableNoteViewModelBuilder<NoteViewModel>, IGetClearParamsBuilder<NoteViewModelBuilder>
    {
        private ICommand _editCommand;
        private NoteViewModel.SmallTaskCanDeleted _question;

        public NoteViewModelBuilder GetClearParamsBuilder() => new NoteViewModelBuilder();

        public NoteViewModelBuilder SetEditCommand(ICommand editCommand)
        {
            _editCommand = editCommand;
            return this;
        }
        public NoteViewModelBuilder SetQuestionConfirmation(NoteViewModel.SmallTaskCanDeleted question)
        {
            _question = question;
            return this;
        }

        protected override NoteViewModel BuildViewModel()
        {
            NoteViewModel noteViewModel = new NoteViewModel(Note ?? new Note());
            noteViewModel.EditSwipeItem.Command = _editCommand;
            noteViewModel.DeleteSwipeItem.Command = DeleteCommand;
            noteViewModel.DeletionConfirmationSmallTask = _question;
            return noteViewModel;
        }
    }
}
