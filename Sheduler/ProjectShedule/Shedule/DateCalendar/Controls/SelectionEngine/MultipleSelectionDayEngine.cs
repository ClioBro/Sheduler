using ProjectShedule.Shedule.DateCalendar.Models;

namespace ProjectShedule.Shedule.Calendar.Controls.SelectionEngine
{
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
}
