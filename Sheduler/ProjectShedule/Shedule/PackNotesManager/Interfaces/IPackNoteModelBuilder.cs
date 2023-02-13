using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.ViewModels.Base;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IPackNoteModelBuilder<out T> : IBuilder<T>
    {
        IPackNoteModelBuilder<T> SetNote(Note note);
        IPackNoteModelBuilder<T> SetSmallTasksBuilder(ISmallTaskViewModelBuilder<BaseSmallTaskViewModel> smallTaskViewModelBuilder);
    }
    public class PackNoteViewModelBuilder<TSmallTaskViewModel> : IBuilder<TSmallTaskViewModel>
    {
        public TSmallTaskViewModel Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
