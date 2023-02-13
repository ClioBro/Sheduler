using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.ViewModels;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface INoteViewModelBuilder<TTypeVM> : IBuilder<TTypeVM>
    {
        INoteViewModelBuilder<TTypeVM> SetData(Note note);
    }
}
