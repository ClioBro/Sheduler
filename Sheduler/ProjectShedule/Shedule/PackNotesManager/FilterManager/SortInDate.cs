using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public abstract class SortInDate : RadioButtonItem, ISortInDate<IPackNote>
    {
        protected IGetQuereblyItems<IPackNote> _getItems;
        public SortInDate(IGetQuereblyItems<IPackNote> getItems)
        {
            _getItems = getItems;
        }
        public virtual IEnumerable<IPackNote> GetItems() => _getItems.GetItems();
    }
    public class SelectedSortInDate : SortInDate
    {
        public SelectedSortInDate(IGetQuereblyItems<IPackNote> getItems) : base(getItems) { }
        public DateTime Date { get; set; }
        public override IEnumerable<IPackNote> GetItems() 
        {
            return _getItems.GetForDateTime(Date);
        }
    }
    public class ToDaySortInDate : SortInDate
    {
        public ToDaySortInDate(IGetQuereblyItems<IPackNote> getItems) : base(getItems)
        {

        }
        public DateTime Date => DateTime.Today;
        public override IEnumerable<IPackNote> GetItems() => _getItems.GetForDateTime(Date);
    }
    public class TomorrowSortInDate : SortInDate
    {
        public TomorrowSortInDate(IGetQuereblyItems<IPackNote> getItems) : base(getItems) { }
        public DateTime Date => DateTime.Today.AddDays(1);
        public override IEnumerable<IPackNote> GetItems() => _getItems.GetForDateTime(Date);
    }
    public class AllSortInDate : SortInDate
    {
        public AllSortInDate(IGetQuereblyItems<IPackNote> getItems) : base(getItems) { }
    }

}
