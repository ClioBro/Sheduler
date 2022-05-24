using ProjectShedule.Shedule.Calendar.Models;

namespace ProjectShedule.Shedule.Calendar.Controls.SelectionEngine
{
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
