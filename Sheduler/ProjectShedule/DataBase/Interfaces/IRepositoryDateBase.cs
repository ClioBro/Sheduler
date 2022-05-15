namespace ProjectShedule.DataBase.Interfaces
{
    public interface IRepositoryDateBase<T> : IGetItems<T>, ISaveItemInDB<T>, IDeleteItemInDB
    {
        //SQLiteConnection database;
    }
}
