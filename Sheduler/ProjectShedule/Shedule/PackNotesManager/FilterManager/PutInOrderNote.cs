using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public abstract class PutInOrderNote: RadioButtonItem, ISortInOrder<IPackNote>
    {
        public abstract IEnumerable<IPackNote> GetSorted(IEnumerable<IPackNote> packNoteModels);
    }
    public class PutInOrderNoteByDate : PutInOrderNote
    {
        public override IEnumerable<IPackNote> GetSorted(IEnumerable<IPackNote> packNoteModels)
        {
            return packNoteModels.OrderBy(P => P.Note.AppointmentDate.Date).ToList();
        }
    }
    public class PutInOrderNoteByAlphabet : PutInOrderNote
    {
        public override IEnumerable<IPackNote> GetSorted(IEnumerable<IPackNote> packNoteModels)
        {
            return packNoteModels.OrderBy(P => P.Note.Header).ToList();
        }
    }
}
