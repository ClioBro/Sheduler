using System.Windows.Input;
using ProjectShedule.Shedule.Interfaces;

namespace ProjectShedule.Shedule.Builder.Base
{
    public abstract class BaseDeletableNoteViewModelBuilder<TNoteViewModel> : BaseNoteViewModelBuilder<TNoteViewModel>, IDeletableViewModelBuilder<BaseDeletableNoteViewModelBuilder<TNoteViewModel>, TNoteViewModel>
    {
        private ICommand _deleteCommand;
        protected ICommand DeleteCommand => _deleteCommand;
        public BaseDeletableNoteViewModelBuilder<TNoteViewModel> SetDeleteCommand(ICommand deleteCommand)
        {
            _deleteCommand = deleteCommand;
            return this;
        }
    }
}
