namespace ProjectShedule.DataBase.Interfaces
{
    public interface ICanReviveItem<T>
    {
        void Revive(T item);
    }
}
