using System.Windows.Input;


namespace ProjectShedule.Shedule.Interfaces
{
    public interface IEditableViewModelBuilder<T> : IBuilder<T>
    {
        IEditableNoteViewModelBuilder<T> SetEditCommand(ICommand editCommand);
    }
}
