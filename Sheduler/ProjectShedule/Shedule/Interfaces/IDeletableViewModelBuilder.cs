using System.Windows.Input;


namespace ProjectShedule.Shedule.Interfaces
{
    public interface IDeletableViewModelBuilder<Tbuilder, out T> : IBuilder<T>
        where Tbuilder : IDeletableViewModelBuilder<Tbuilder, T>
    {
        Tbuilder SetDeleteCommand(ICommand deleteCommand);
    }
}
