using System.Windows.Input;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IPackNoteControl
    {
        ICommand DeleteMeCommand { get; }
        ICommand EditMeCommand { get; }
    }
}
