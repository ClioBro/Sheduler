using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models.Base;
using ProjectShedule.Shedule.ViewModels;
using System.Windows.Input;


namespace ProjectShedule.Shedule.Builder.Base
{
    public abstract class BaseDeletableSmallTaskViewModelBuilder<T> : BaseSmallTaskViewModel<T>, IDeletableViewModelBuilder<BaseDeletableSmallTaskViewModelBuilder<T>, T>
        where T : DeletableSmallTaskViewModel
    {
        private ICommand _deleteCommand;
        protected BaseDeletableSmallTaskViewModelBuilder(ISmallTaskModelBuilder smallTaskModelBuilder) : base(smallTaskModelBuilder)
        {
        }
        protected ICommand DeleteCommand => _deleteCommand;

        public BaseDeletableSmallTaskViewModelBuilder<T> SetDeleteCommand(ICommand deleteCommand)
        {
            _deleteCommand = deleteCommand;
            return this;
        }
    }
}
