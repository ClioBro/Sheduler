using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Interfaces;
using System.Collections.Specialized;
using System.Linq;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public abstract class NoteDemonstrationViewModel<TSmallTaskViewModel> : BaseExtandedNoteViewModel<TSmallTaskViewModel>
        where TSmallTaskViewModel : SimpleSmallTaskViewModel
    {
        public NoteDemonstrationViewModel(Note note) : base(note)
        {

        }

        public bool HasSmallTasks => ReadOnlySmallTaskViewModels.Count() > 0;
        public string TasksCompletedInformation => $"{ReadOnlySmallTaskViewModels.Count(t => t.Status)}/{ReadOnlySmallTaskViewModels.Count}";

        protected override TSmallTaskViewModel BuildViewModel(SmallTask smallTask)
        {
            TSmallTaskViewModel smallTaskViewModel = base.BuildViewModel(smallTask);
            smallTaskViewModel.StatusChanged += SmallTaskViewModel_StatusChanged;
            return smallTaskViewModel;
        }

        protected virtual void SmallTaskViewModel_StatusChanged(IDemonstrationSmallTaskViewModel smallTaskViewModel, bool value)
        {
            OnPropertyChanged(nameof(TasksCompletedInformation));
        }
        protected override void SmallTaskViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.SmallTaskViewModels_CollectionChanged(sender, e);
            OnPropertyChanged(nameof(HasSmallTasks), nameof(TasksCompletedInformation));
        }
    }
}
