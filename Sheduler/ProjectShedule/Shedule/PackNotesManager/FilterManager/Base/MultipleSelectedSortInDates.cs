using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.Base
{
    public abstract class MultipleSelectedSortInDates : SortBaseNote
    {
        public MultipleSelectedSortInDates(IGetItemsDateTime<Note> getItems, IEnumerable<DateTime> dateTimes) 
            : base(getItems) 
        {
            Dates= dateTimes;
        }

        public IEnumerable<DateTime> Dates { get; set; }
    }
}
