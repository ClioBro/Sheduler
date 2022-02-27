using ProjectShedule.Shedule.NotifyOnApp.Enum;

namespace ProjectShedule.Shedule.Models
{
    public class RepeadItem : RadioButtonItem
    {
        public RepeadType RepeadType { get; set; }
    }

    public class RadioButtonItem
    {
        public string Text { get; set; }
        public bool IsChecked { get; set; }
        public RadioButtonItem This => this;
    }
}
