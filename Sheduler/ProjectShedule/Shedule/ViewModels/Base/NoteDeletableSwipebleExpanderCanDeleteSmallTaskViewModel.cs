using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.PopUpAlert.Question;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public abstract class NoteDeletableSwipebleExpanderCanDeleteSmallTaskViewModel<TDeletableSmallTask> : NoteDeletableSwipebleExpanderViewModel<TDeletableSmallTask>
        where TDeletableSmallTask : DeletableSmallTaskViewModel
    {
        private ICommand _deleteSmallTaskCommand;

        public delegate Task<QuestionView.Answer> SmallTaskCanDeleted(TDeletableSmallTask smallTaskViewModel);
        public event SmallTaskEventHandler<NoteDeletableSwipebleExpanderCanDeleteSmallTaskViewModel<TDeletableSmallTask>, TDeletableSmallTask> SmallTaskDeletePressed;

        public NoteDeletableSwipebleExpanderCanDeleteSmallTaskViewModel(Note note) : base(note) { }
        public SmallTaskCanDeleted DeletionConfirmationSmallTask { get; set; }
        public int DeletedSmallTasksCount => SmallTaskViewModels.Where(s => s.IsDeleted).Count();

        protected override void InicializationCommands()
        {
            _deleteSmallTaskCommand = new Command<TDeletableSmallTask>(TryDeleteAsyncCommandHandler);
        }
        protected override TDeletableSmallTask BuildViewModel(SmallTask smallTask)
        {
            TDeletableSmallTask smallTaskViewModel = base.BuildViewModel(smallTask);
            smallTaskViewModel.DeleteSwipeItem.Command = _deleteSmallTaskCommand;
            return smallTaskViewModel;
        }

        private async void TryDeleteAsyncCommandHandler(TDeletableSmallTask simpleSmallTaskViewModel)
        {
            if (DeletionConfirmationSmallTask != null)
            {
                var result = await DeletionConfirmationSmallTask.Invoke(simpleSmallTaskViewModel);
                if (result.Value == false)
                    return;
            }
            Delete(simpleSmallTaskViewModel);
            SmallTaskDeletePressed?.Invoke(this, simpleSmallTaskViewModel);
        }
        private void Delete(TDeletableSmallTask smallTaskViewModel)
        {
            SmallTaskViewModels.Remove(smallTaskViewModel);
        }
    }
}
