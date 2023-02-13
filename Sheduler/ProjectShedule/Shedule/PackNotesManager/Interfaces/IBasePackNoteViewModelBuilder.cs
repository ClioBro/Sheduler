using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IBasePackNoteViewModelBuilder<out VM, M> : IBuilder<VM>
    {
        IBasePackNoteViewModelBuilder<VM, M> SetModel(M basePackNoteModel);
        IBasePackNoteViewModelBuilder<VM, M> SetNavigation(INavigation navigation);
    }
    public interface IBasePackNoteDeletebleViewModelBuilder<out VM, M> : IBasePackNoteViewModelBuilder<VM, M>
    {
        IBasePackNoteDeletebleViewModelBuilder<VM, M> SetDeleteMeCommand(ICommand deleteMeCommand);
    }
    public interface IPackNoteViewModelBuilder<out VM, M> : IBasePackNoteDeletebleViewModelBuilder<VM, M>
    {
        IPackNoteViewModelBuilder<VM, M> SetEditMeCommand(ICommand editMeCommand);
    }
    public interface IThrashPackNoteViewModelBuilder<out VM, M> : IBasePackNoteDeletebleViewModelBuilder<VM, M>
    {
        IThrashPackNoteViewModelBuilder<VM, M> SetReviveMeCommand(ICommand reviveMeCommand);
    }
}
