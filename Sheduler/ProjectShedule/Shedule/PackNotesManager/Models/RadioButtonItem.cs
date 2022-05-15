using ProjectShedule.Shedule.Interfaces;

namespace ProjectShedule.Shedule.Models
{
    public class RadioButtonItem : IRadioButtonItem
    {
        public string Text { get; set; }
        public bool IsChecked { get; set; }
        public RadioButtonItem This => this;
    }
}
