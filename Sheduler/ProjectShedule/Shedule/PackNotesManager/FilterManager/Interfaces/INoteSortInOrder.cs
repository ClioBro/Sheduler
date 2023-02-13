using ProjectShedule.DataBase.Interfaces;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.Interfaces
{
    public interface INoteSortInOrder
    {
       public IEnumerable<T> SortNoteInOrder<T>(IEnumerable<T> itemSort) where T : INote;
    }
}
