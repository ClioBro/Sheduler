using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;
using System.ComponentModel;

namespace ProjectShedule.Shedule.ViewModels.Interfaces
{
    public interface INoteViewModel : INotifyPropertyChanged, INote, IHasData<Note>, IHasHeader, INoteVisualColor
    {

    }

}
