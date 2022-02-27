using ProjectShedule.Shedule.ViewModels;
using System.Collections.ObjectModel;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IHasTaskViewModels
    {
        public ObservableCollection<SmallTaskViewModel> SmallTasks { get; set; }
    }
}
