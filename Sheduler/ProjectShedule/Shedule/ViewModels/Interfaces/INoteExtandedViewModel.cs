using ProjectShedule.Shedule.Interfaces;
using System.Collections.ObjectModel;

namespace ProjectShedule.Shedule.ViewModels.Interfaces
{
    public interface INoteExtandedViewModel<TSmallTaskViewModel> : INoteViewModel
        where TSmallTaskViewModel : IDemonstrationSmallTaskViewModel
    {
        ReadOnlyObservableCollection<TSmallTaskViewModel> ReadOnlySmallTaskViewModels { get; }
    }
}
