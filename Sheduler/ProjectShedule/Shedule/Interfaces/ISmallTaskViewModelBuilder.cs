using ProjectShedule.DataBase.BusinessLayer.Entities;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface ISmallTaskViewModelBuilder<out T> : IBuilder<T> where T : IDemonstrationSmallTaskViewModel
    {
        ISmallTaskViewModelBuilder<T> SetData(SmallTask smallTask);
    }
}
