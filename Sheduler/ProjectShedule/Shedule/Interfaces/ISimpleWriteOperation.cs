namespace ProjectShedule.Shedule.Interfaces
{
    public interface ISimpleWriteOperation<T> : ISave<T>, IDelete<T>
    {
    }
}