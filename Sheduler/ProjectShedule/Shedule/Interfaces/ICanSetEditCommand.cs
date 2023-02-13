using System.Windows.Input;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface ICanSetEditCommand<T>
    {
        T SetEditCommand(ICommand editCommand);
    }
}
