using ProjectShedule.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectShedule.Calendar.Controls.SelectionEngine
{
    public class SelectedDateControll
    {
        private DayModel _oldDay;
        private DayModel _newDay;
        private DateTime? _selectedDate;

        public DateTime? GetSeletedDate()
        {
            return _selectedDate;
        }
        public void SelectDay(DayModel newDay)
        {
            _newDay = newDay;
            _newDay.IsSelected = !_newDay.IsSelected;

            if (_newDay != _oldDay && _oldDay != null)
                _oldDay.IsSelected = false;

            SelectDate(_newDay.Date);

            _oldDay = _newDay;
            _newDay = null;
        }
        public bool IsDateSelected(DateTime date)
        {
            return Equals(date, _selectedDate);
        }

        private void SelectDate(DateTime dateTime)
        {
            if (dateTime != _selectedDate)
                _selectedDate = dateTime;
            else
                _selectedDate = null;
        }
    }
}
