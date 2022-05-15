using ProjectShedule.Shedule.Models;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IRadioButtonItem
    {
        string Text { get; }
        bool IsChecked { get; }
        RadioButtonItem This { get; }
    }
}
