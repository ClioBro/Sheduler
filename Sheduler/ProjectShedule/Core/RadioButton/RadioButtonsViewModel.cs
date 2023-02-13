using System;
using System.Linq;

namespace ProjectShedule.Core.RadioButton
{
    public class RadioButtonsViewModel : FlexLayoutViewModel, IRadioButtonsViewModel
    {
        private RadioButtonItemModel _selectedItem;

        public RadioButtonItemModel[] Items { get; protected set; }
        public virtual RadioButtonItemModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                    return;
                if (Items.Contains(value) == false)
                    throw new ArgumentException("Value not in collection");
                _selectedItem = value;
                NotifyProperty(nameof(SelectedItem));
            }
        }
        public string GroupName { get; protected set; }
    }
}