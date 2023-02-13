using ProjectShedule.Core.Sorting;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.States;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.PutInOrder
{
    public class SortNoteByAlphabetically : SortInOrderNote, INoteSortInOrder
    {
        public SortNoteByAlphabetically()
        {
            Text = Language.Resources.OtherElements.Filters.ByAlphabetically;
            OrderByState = new DescendingOrderByState();
        }

        public override OrderState OrderByState { get; set; }

        public override IEnumerable<T> SortNoteInOrder<T>(IEnumerable<T> filteredItem)
        {
            var result = OrderByState.Sort(filteredItem, GetHeader);
            return result;
        }

        private string GetHeader<T>(T note) where T : INote => note.Header;
    }
}