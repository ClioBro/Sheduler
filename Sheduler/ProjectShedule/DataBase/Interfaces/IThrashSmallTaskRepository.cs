using ProjectShedule.DataBase.BusinessLayer.Entities;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IThrashSmallTaskDataBase : IDataBase<SmallTask>, ICanReviveItem<SmallTask>
    {

    }
}
