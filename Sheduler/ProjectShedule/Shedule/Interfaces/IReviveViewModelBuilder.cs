using System.Windows.Input;


namespace ProjectShedule.Shedule.Interfaces
{
    public interface IReviveViewModelBuilder<Tbuilder, out T> : IBuilder<T>
         where Tbuilder : IReviveViewModelBuilder<Tbuilder, T>
    {
        Tbuilder SetReviveCommand(ICommand reviveCommand);
    }
}
