using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Repositories;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IDateBaseQueryableDateTime<TType> : IDataBaseGetByDateTime<TType>, IGetItemsQueryableDateTime<TType>
    {

    }
    public interface IDataBaseGetByDateTime<TType> : IDataBase<TType>, IGetItemsDateTime<TType>
    {

    }
    public interface IExtandedLiveNoteDataBase : IDataBaseGetByDateTime<Note>
    {
        IDataBaseGetByDateTime<Note> NoteDataBase { get; }
        IDataBase<SmallTask> SmallTaskDataBase { get; }
    }
    public interface IExtandedDeadNoteDataBase : IDataBaseGetByDateTime<Note>, ICanReviveItem<Note>
    {
        IDeadDataBaseByDateTimeTable<Note> NoteDataBase { get; }
        IDeadDataBase<SmallTask> SmallTaskDataBase { get; }
    }
}
