using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.DateCalendar.Controls.SelectionEngine;
using System;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProjectShedule.Shedule.Calendar.Controls.SelectionEngine
{
    public abstract class BaseSelectionHasDateEngine<T> : ISelectionEngine<T> where T : IHasDate
    {
        protected ObservableRangeCollection<DateTime> _selectedItems = new ObservableRangeCollection<DateTime>();
        public ObservableRangeCollection<DateTime> SelectedDatesTime { get => _selectedItems; set => _selectedItems = value; }
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
        private DayModel _selectedDayModel;
        public override void SelectItem(DayModel newDay)
        {
            newDay.IsSelected = !newDay.IsSelected;

            if (_selectedItems.Contains(newDay.Date))
            {
                _selectedDayModel = null;
                _selectedItems.Clear();
            }
            else
            {
                if (_selectedDayModel != null)
                    _selectedDayModel.IsSelected = !_selectedDayModel.IsSelected;
                _selectedDayModel = newDay;
                _selectedItems.Replace(_selectedDayModel.Date);
            }
                
        }
    }
}
