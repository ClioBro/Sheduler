using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.Models
{
    public class ReadOnlyPackNote : IReadOnlyPackNote
    {
        private readonly INote _note;
        private readonly IEnumerable<ISmallTask> _smallTasks;
        public ReadOnlyPackNote(INote note, IEnumerable<ISmallTask> smallTasks)
        {
            _note = note;
            _smallTasks = smallTasks;
        }
        public INote Note => _note;
        public IEnumerable<ISmallTask> SmallTasks => _smallTasks;
    }
}
