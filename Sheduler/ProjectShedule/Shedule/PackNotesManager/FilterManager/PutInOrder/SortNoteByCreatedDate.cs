using ProjectShedule.Core.Sorting;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.States;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.PutInOrder
{
    public class SortNoteByCreatedDate : SortInOrderNote
    {
        public SortNoteByCreatedDate()
        {
            Text = Language.Resources.OtherElements.Filters.ByCreatedDate;
            OrderByState = new DescendingOrderByState();
        }

        public override OrderState OrderByState { get; set; }

        public override IEnumerable<T> SortNoteInOrder<T>(IEnumerable<T> filteredItem)
        {
            var result = OrderByState.Sort(filteredItem, GetCreatedDate);
            return result;
        }

        private DateTime GetCreatedDate<T>(T note) where T : INote => note.CreatedDateTime;
    }
}