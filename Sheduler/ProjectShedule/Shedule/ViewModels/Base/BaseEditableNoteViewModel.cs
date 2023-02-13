using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.PopUpAlert.Question;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public abstract class BaseEditableNoteViewModel<TSmallTaskViewModel> : NoteDemonstrationViewModel<TSmallTaskViewModel>
        where TSmallTaskViewModel : DeletableSmallTaskViewModel
    {
        public delegate Task<bool> SmallTaskCanDeleted(TSmallTaskViewModel smallTaskViewModel);
        private ICommand _deleteSmallTaskCommand;
        private ICommand _createSmallTaskCommand;

        public BaseEditableNoteViewModel(Note note) : base(note) { }

        public SmallTaskCanDeleted DeletionConfirmationSmallTask { get; set; }
        public ICommand CreateSmallTaskCommand => _createSmallTaskCommand;

        protected override void InicializationCommands()
        {
            _createSmallTaskCommand = new Command<string>(CreateSmallTask);
            _deleteSmallTaskCommand = new Command<TSmallTaskViewModel>(TryDeleteAsync);
        }
        protected override void SmallTaskViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                IEnumerable<TSmallTaskViewModel> newItems = e.NewItems.OfType<TSmallTaskViewModel>();
                IEnumerable<SmallTask> newSmallTasks = newItems.Select(s => GetSmallTask(s));
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Note.SmallTasks.AddRange(newSmallTasks);
                        break;
                    default:
                        throw new ArgumentException($"Not processed: {e.Action}", nameof(e.Action));
                }
            }
            base.SmallTaskViewModels_CollectionChanged(sender, e);
        }
        protected override TSmallTaskViewModel BuildViewModel(SmallTask smallTask)
        {
            TSmallTaskViewModel smallTaskViewModel = base.BuildViewModel(smallTask);
            smallTaskViewModel.DeleteSwipeItem.Command = _deleteSmallTaskCommand;
            return smallTaskViewModel;
        }

        private void CreateSmallTask(string text)
        {
            SmallTaskViewModels.Add(BuildViewModel(new SmallTask() { Header = text }));
        }
        private async void TryDeleteAsync(TSmallTaskViewModel simpleSmallTaskViewModel)
        {
            if (DeletionConfirmationSmallTask != null)
            {
                var result = await DeletionConfirmationSmallTask.Invoke(simpleSmallTaskViewModel);
                if (result == false)
                    return;
            }

            Delete(simpleSmallTaskViewModel);
        }
        private void Delete(TSmallTaskViewModel smallTaskViewModel)
        {
            SmallTaskViewModels.Remove(smallTaskViewModel);
        }
    }
}
