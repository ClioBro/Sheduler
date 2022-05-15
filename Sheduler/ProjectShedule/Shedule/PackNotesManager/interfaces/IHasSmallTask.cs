using ProjectShedule.DataBase.Entities.Base;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IHasSmallTask
    {
        BaseSmallTask SmallTask { get; }
    }
}
