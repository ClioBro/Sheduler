namespace ProjectShedule.DataBase.Interfaces
{
    public interface IGetItemsQueryableDateTime<out T> : IGetItemsDateTime<T>, IQueryable<T>
    {

    }
    public interface IGetItemsDateTime<out T> : IReadDataOperation<T>, IGetByDateTime<T>
    {

    }

}
