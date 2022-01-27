using ProjectShedule.Shedule.Enum;

namespace ProjectShedule.Shedule.Models
{
    public class Repead
    {
        public RepeadType RepeadType { get; set; }
        public Repead This => this;
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
}
