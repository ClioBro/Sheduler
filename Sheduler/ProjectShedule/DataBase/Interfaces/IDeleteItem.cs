namespace ProjectShedule.DataBase.Interfaces
{
    public interface IDeleteItem<T>
    {
        void Delete(T item);
    }
}
