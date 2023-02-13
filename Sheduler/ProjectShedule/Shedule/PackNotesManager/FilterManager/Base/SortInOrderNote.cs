using ProjectShedule.Core.RadioButton;
using ProjectShedule.Core.Sorting;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Interfaces;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.Base
{
    public abstract class SortInOrderNote : RadioButtonItemModel, INoteSortInOrder, IHasOrderByState
    {
        public abstract OrderState OrderByState { get; set; }
        public virtual bool Descending
        {
            get => OrderByState.Descending;
            set
            {
                if (OrderByState.Descending == value)
                    return;
                SwichState();
            }
        }

        public abstract IEnumerable<T> SortNoteInOrder<T>(IEnumerable<T> filteredItem) where T : INote;
        private void SwichState() => OrderByState.SwichOrderBy(this);
    }
}
