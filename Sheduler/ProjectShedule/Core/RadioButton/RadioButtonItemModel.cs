using System;

namespace ProjectShedule.Core.RadioButton
{
    public class RadioButtonItemModel : IRadioButtonItem
    {
        private bool _checked;

        public event EventHandler<bool> CheckedChanged;

        public virtual string Text { get; set; }
        public virtual bool IsChecked 
        {
            get => _checked;
            set
            {
                if (_checked == value)
                    return;
                _checked= value;
                CheckedChanged?.Invoke(this, value);
            }
        }
        public RadioButtonItemModel This => this;
    }
}
