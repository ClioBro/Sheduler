using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using System;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.Base
{
    public abstract class SingleSelectedSortInDate : SortBaseNote
    {
        public SingleSelectedSortInDate(IGetItemsDateTime<Note> getItems, DateTime dateTime) 
            : base(getItems) 
        {
            Date= dateTime;
        }
        public DateTime Date { get; set; }
    }
}
