using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Builder;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ProjectShedule.Shedule.ViewModels
{
    public class EditNoteViewModel : BaseEditableNoteViewModel<DeletableSmallTaskViewModel>
    {
        private readonly IEnumerable<SmallTask> _tempMemorySmallTasks;
        private readonly List<DeletableSmallTaskViewModel> _removedOldSmallTasks;

        public EditNoteViewModel(Note note) : base(note)
        {
            _tempMemorySmallTasks = new List<SmallTask>(note.SmallTasks);
            _removedOldSmallTasks = new List<DeletableSmallTaskViewModel>();
        }

        public IEnumerable<DeletableSmallTaskViewModel> RemovedOldSmallTasks => _removedOldSmallTasks;
        public override ISmallTaskViewModelBuilder<DeletableSmallTaskViewModel> SmallTaskViewModelBuilder { get; protected set; } = new DeletableSmallTaskViewModelBuilder();

        public override object Clone()
        {
            return new EditNoteViewModel(Note.Clone() as Note);
        }

        protected override void SmallTaskViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.SmallTaskViewModels_CollectionChanged(sender, e);

            if (e.OldItems != null)
            {
                IEnumerable<DeletableSmallTaskViewModel> oldDeletableSmallTaskViewModels = e.OldItems.OfType<DeletableSmallTaskViewModel>();

                foreach (DeletableSmallTaskViewModel deletableSmallTaskViewModel in oldDeletableSmallTaskViewModels)
                {
                    IHasData<SmallTask> hasDataSmallTask = deletableSmallTaskViewModel;
                    SmallTask smallTask = hasDataSmallTask.GetData();
                    if (_tempMemorySmallTasks.Contains(smallTask))
                    {
                        _removedOldSmallTasks.Add(deletableSmallTaskViewModel);
                    }
                }
            }
        }
    }
}
