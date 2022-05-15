using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.DateCalendar.Controls.SelectionEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ProjectShedule.Shedule.Calendar.Controls.SelectionEngine
{
    public abstract class BaseSelectionDateEngine<T> : ISelectionEngine<T> where T : IHasDate
    {
        protected readonly ObservableCollection<DateTime> _selectedItems;
        public BaseSelectionDateEngine()
        {
            _selectedItems = new ObservableCollection<DateTime>();
        }
        public ReadOnlyObservableCollection<DateTime> SelectedDatesTime
        {
            get => new ReadOnlyObservableCollection<DateTime>(_selectedItems);
        }
        public abstract void SelectItem(T newItem);
        public bool ItemIsSelectet(T item) => _selectedItems.Contains(item.Date);
        public bool DateIsSelected(DateTime dateTime) => _selectedItems.Contains(dateTime);
    }
    internal class MultipleSelectionDayEngine : BaseSelectionDateEngine<DayModel>
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
    internal class SingleSelectionDayEngine : BaseSelectionDateEngine<DayModel>
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
