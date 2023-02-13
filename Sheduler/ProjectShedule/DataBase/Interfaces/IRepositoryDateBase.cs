namespace ProjectShedule.DataBase.Interfaces
{
    public interface IDataBase<T> : IWriteDataOperation<T>, IReadDataOperation<T>
    {

    }
}
