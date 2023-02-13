namespace ProjectShedule.Shedule.Interfaces
{
    public interface IThrashWriteOperation<T> : IDelete<T>, IRevive<T>
    {

    }
}