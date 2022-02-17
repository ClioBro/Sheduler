using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public abstract class SortInDate : RadioButtonItem, ISortInDate<IPackNote>
    {
        public virtual List<IPackNote> GetItems() => GetAll();
        private protected List<IPackNote> GetAll()
        {
            var repository = new PackNoteRepository();
            return repository.GetAll();
        }
        private protected List<IPackNote> GetForDate(DateTime dateTime)
        {
            var repository = new PackNoteRepository();
            return repository.GetForDate(dateTime);
        }
    }
    public class SelectedSortInDate : SortInDate
    {
        public DateTime Date { get; set; }
        public override List<IPackNote> GetItems() => GetForDate(Date);
    }
    public class ToDaySortInDate : SortInDate
    {
        public DateTime Date => DateTime.Today;
        public override List<IPackNote> GetItems() => GetForDate(Date);
    }
    public class TomorrowSortInDate : SortInDate
    {
        public DateTime Date => DateTime.Today.AddDays(1);
        public override List<IPackNote> GetItems() => GetForDate(Date);
    }
    public class AllSortInDate : SortInDate
    {
        public override List<IPackNote> GetItems() => GetAll();
    }

}
