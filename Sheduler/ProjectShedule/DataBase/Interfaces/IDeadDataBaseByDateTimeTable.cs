using ProjectShedule.DataBase.Repositories;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IDeadDataBaseByDateTimeTable<T> : IDataBaseGetByDateTime<T>, IDeadDataBase<T>
    {

    }
}
