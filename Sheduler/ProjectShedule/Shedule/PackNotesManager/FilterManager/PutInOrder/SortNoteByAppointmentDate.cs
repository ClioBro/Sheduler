using ProjectShedule.Core.Sorting;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.States;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.PutInOrder
{
    public class SortNoteByAppointmentDate : SortInOrderNote
    {
        public SortNoteByAppointmentDate()
        {
            Text = Language.Resources.OtherElements.Filters.ByDateAppointment;
            OrderByState = new DescendingOrderByState();
        }

        public override OrderState OrderByState { get; set; }

        public override IEnumerable<T> SortNoteInOrder<T>(IEnumerable<T> filteredItem)
        {
            var result = OrderByState.Sort(filteredItem, GetAppointmentDate);
            return result;
        }

        private DateTime GetAppointmentDate<T>(T note) where T : INote => note.AppointmentDate.Value;
    }
}