using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.DateCalendar.Controls.SelectionEngine;
using System;
using System.Collections.ObjectModel;

namespace ProjectShedule.Shedule.Calendar.Controls.SelectionEngine
{
    public abstract class BaseSelectionHasDateEngine<T> : ISelectionEngine<T> where T : IHasDate
    {
        protected ObservableCollection<DateTime> _selectedItems = new ObservableCollection<DateTime>();
        public ObservableCollection<DateTime> SelectedDatesTime { get => _selectedItems; set => _selectedItems = value; }
        public abstract void SelectItem(T newItem);
        public bool ItemIsSelectet(T item) => _selectedItems.Contains(item.Date);
        public bool DateIsSelected(DateTime dateTime) => _selectedItems.Contains(dateTime);
    }
    internal class MultipleSelectionDayEngine : BaseSelectionHasDateEngine<DayModel>
    {
        public override void SelectItem(DayModel newDay)
        {
            newDay.IsSelected = !newDay.IsSelected;

            if (_selectedItems.Contains(newDay.Date))
                _selectedItems.Remove(newDay.Date);
            else
                _selectedItems.Add(newDay.Date);
        }
    }
    internal class SingleSelectionDayEngine : BaseSelectionHasDateEngine<DayModel>
    {
        public override void SelectItem(DayModel newDay)
        {
            newDay.IsSelected = !newDay.IsSelected;

            if (_selectedItems.Contains(newDay.Date))
                _selectedItems.Clear();
            else
                _selectedItems.Insert(0, newDay.Date);
        }
    }
}
