using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public abstract class PutInOrder : RadioButtonItem, ISortInOrder<PackNoteModel>
    {
        public abstract List<PackNoteModel> GetSorted(List<PackNoteModel> packNoteModels);
    }
    public class PutInOrderByDate : PutInOrder
    {
        public override List<PackNoteModel> GetSorted(List<PackNoteModel> packNoteModels)
        {
            return packNoteModels.OrderBy(P => P.Note.AppointmentDate.Date).ToList();
        }
    }
    public class PutInOrderByAlphabet : PutInOrder
    {
        public override List<PackNoteModel> GetSorted(List<PackNoteModel> packNoteModels)
        {
            return packNoteModels.OrderBy(P => P.Note.Header).ToList();
        }
    }
}
