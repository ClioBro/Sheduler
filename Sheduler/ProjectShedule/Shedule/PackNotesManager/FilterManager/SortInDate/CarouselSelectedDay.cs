using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Language.Resources.OtherElements;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public class CarouselSelectedDay : SingleSelectedSortInDate
    {
        public CarouselSelectedDay(IGetItemsDateTime<Note> getItems, DateTime dateTime) 
            : base(getItems, dateTime) 
        {
            Text = Filters.ByCarouselDate;
        }
        public override IEnumerable<Note> Filter() => GetItemsDateTime.GetByDate(Date);
    }
}
