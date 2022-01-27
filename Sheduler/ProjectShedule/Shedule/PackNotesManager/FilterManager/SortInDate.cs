using ProjectShedule.DataNote;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public class SortInDate : ISortInDate<SortInDate>
    {
        public SortInDate ThisType => this;
        public string Text { get; set; }
        public bool IsSelected { get; set; }
        public virtual List<PackNoteModel> GetItems() => GetAll();
        private protected List<PackNoteModel> GetAll()
        {
            var manager = new PackNoteDBManager();
            return manager.GetAll();
        }
        private protected List<PackNoteModel> GetForDate(DateTime dateTime)
        {
            var manager = new PackNoteDBManager();
            return manager.GetForDate(dateTime);
        }
    }
    public class SelectedSortInDate : SortInDate
    {
        public DateTime Date { get; set; }
        public override List<PackNoteModel> GetItems() => GetForDate(Date);
    }
    public class ToDaySortInDate : SortInDate
    {
        public DateTime Date => DateTime.Today;
        public override List<PackNoteModel> GetItems() => GetForDate(Date);
    }
    public class TomorrowSortInDate : SortInDate 
    {
        public DateTime Date => DateTime.Today.AddDays(1);
        public override List<PackNoteModel> GetItems() => GetForDate(Date);
    }
    public class AllSortInDate : SortInDate
    {
        public override List<PackNoteModel> GetItems() => GetAll();
    }

}
