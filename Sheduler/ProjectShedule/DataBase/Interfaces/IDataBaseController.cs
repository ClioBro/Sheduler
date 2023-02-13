namespace ProjectShedule.DataBase.Interfaces
{
    public interface IDataBaseController<T> : IInsertItem<T>, IUpdateItem<T>, IDeleteItem<T>
    {

    }
}
