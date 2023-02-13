using System.Windows.Input;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface ICanSetDeleteCommand<T>
    {
        T SetDeleteCommand(ICommand deleteCommand);
    }
}
