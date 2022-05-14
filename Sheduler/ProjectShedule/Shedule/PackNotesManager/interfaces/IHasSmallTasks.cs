using ProjectShedule.DataNote;
using ProjectShedule.Shedule.ViewModels;
using System.Collections.ObjectModel;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IHasSmallTasks
    {
        public ReadOnlyObservableCollection<SmallTaskViewModel> SmallTasks { get; }
    }
}