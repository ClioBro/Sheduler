namespace ProjectShedule.Core.Notify
{
    public interface INotifyManager<T>
    {
        void SendNotify(T item);
        void RemoveNotify(T item);
    }
}