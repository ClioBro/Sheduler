using ProjectShedule.DataBase.BusinessLayer.Entities.Base;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface INoteRepository<T> : IDataBase<T>, IGetItemsQueryableDateTime<T> where T : BaseNote
    {

    }
}
