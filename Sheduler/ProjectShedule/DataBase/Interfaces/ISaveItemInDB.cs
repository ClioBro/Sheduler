namespace ProjectShedule.DataBase.Interfaces
{
    public interface ISaveItemInDB<T>
    {
        int SaveItem(T item);
    }
}
