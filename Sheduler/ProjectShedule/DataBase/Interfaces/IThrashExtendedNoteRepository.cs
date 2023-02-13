using ProjectShedule.DataBase.BusinessLayer.Entities;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IThrashExtendedNoteDataBase : IDateBaseQueryableDateTime<Note>, ICanReviveItem<Note>
    {
        IThrashSmallTaskDataBase ThrashSmallTaskDataBase { get; }
    }
}
