using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public abstract class SortInDate : RadioButtonItem, ISortInDate<IPackNote>
    {
        protected IGetQuereblyItems<IPackNote> _getItems;
        public SortInDate(IGetQuereblyItems<IPackNote> getItems) => _getItems = getItems;
        public virtual IEnumerable<IPackNote> GetItems() => _getItems.GetItems();
    }

    public class MultipleSelectedSortInDates : SortInDate
    {
        public MultipleSelectedSortInDates(IGetQuereblyItems<IPackNote> getItems) : base(getItems) { }
        public IEnumerable<DateTime> Dates { get; set; }
    }
    public class SingleSelectedSortInDate : SortInDate
    {
        public SingleSelectedSortInDate(IGetQuereblyItems<IPackNote> getItems) : base(getItems) { }
        public DateTime Date { get; set; }
    }

    public class CalendarSelectedDays : MultipleSelectedSortInDates
    {
        public CalendarSelectedDays(IGetQuereblyItems<IPackNote> getItems) : base(getItems) { }
        public override IEnumerable<IPackNote> GetItems() =>_getItems.GetForDates(Dates);
    }
    public class CarouselSelectedDay : SingleSelectedSortInDate
    {
        public CarouselSelectedDay(IGetQuereblyItems<IPackNote> getItems) : base(getItems) { }
        public override IEnumerable<IPackNote> GetItems() => _getItems.GetForDate(Date);
    }
    public class AllSortInDate : SortInDate
    {
        public AllSortInDate(IGetQuereblyItems<IPackNote> getItems) : base(getItems) { }
    }


    //public class ToDaySortInDate : SingleSelectedSortInDate
    //{
    //    public ToDaySortInDate(IGetQuereblyItems<IPackNote> getItems) : base(getItems)
    //    {
    //        Date = DateTime.Today;
    //    }
    //    public override IEnumerable<IPackNote> GetItems() => _getItems.GetForDate(Date);
    //}
    //public class TomorrowSortInDate : SingleSelectedSortInDate
    //{
    //    public TomorrowSortInDate(IGetQuereblyItems<IPackNote> getItems) : base(getItems)
    //    {
    //        Date = DateTime.Today.AddDays(1);
    //    }
    //    public override IEnumerable<IPackNote> GetItems() => _getItems.GetForDate(Date);
    //}

}
