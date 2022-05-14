using ProjectShedule.Shedule.ViewModels;
using System.Collections.ObjectModel;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IHasSmallTasks
    {
        ReadOnlyObservableCollection<BaseSmallTaskViewModel> SmallTasks { get; }
        void DeleteSmallTask(BaseSmallTaskViewModel smallTask);
        void AddSmallTask(BaseSmallTaskViewModel smallTask);
    }
}