using ProjectShedule.DataBase.Interfaces;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IReadOnlyPackNote
    {
        INote Note { get; }
        IEnumerable<ISmallTask> SmallTasks { get; }
    }
}
