namespace ProjectShedule.DataBase.Interfaces
{
    public interface IDataBaseController<T> : IGetQuereblyItems<T>
    {
        void Save(T packNote);
        void Delete(T packNote);
    }
}
