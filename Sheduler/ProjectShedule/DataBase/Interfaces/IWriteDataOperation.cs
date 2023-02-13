namespace ProjectShedule.DataBase.Interfaces
{
    public interface IWriteDataOperation<T> : IInsertItem<T>, IUpdateItem<T>, IDeleteItemByID, IDeleteItem<T>
    {

    }
}
