using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Language.Resources.OtherElements;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public class CalendarSelectedDays : MultipleSelectedSortInDates
    {
        public CalendarSelectedDays(IGetItemsDateTime<Note> getItems, IEnumerable<DateTime> dateTimes) 
            : base(getItems, dateTimes) 
        {
            Text = Filters.ByCalendar;
        }

        public override IEnumerable<Note> Filter() => GetItemsDateTime.GetByDates(Dates);
    }
}
