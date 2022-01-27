using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public class PutInOrder : ISortInOrder<PutInOrder>
    {
        public PutInOrder ThisType => this;
        public string Text { get; set; }
        public bool IsSelected { get; set; }
        public virtual List<PackNoteModel> GetSorted(List<PackNoteModel> packNoteModels) 
        {
            return packNoteModels;
        }
    }
    public class PutInOrderByDate : PutInOrder
    {
        public override List<PackNoteModel> GetSorted(List<PackNoteModel> packNoteModels)
        {
            return packNoteModels.Where(P => P.Note.DateTimeStatus).OrderBy(P => P.Note.AppointmentDate.Date).ToList();
        }
    }
    public class PutInOrderByAlphabet : PutInOrder
    {
        public override List<PackNoteModel> GetSorted(List<PackNoteModel> packNoteModels)
        {
            return packNoteModels.OrderBy(P => P.Note.Header).ToList();
        }
    }
    public class PutInOrderByWithoutDate : PutInOrder
    {
        public override List<PackNoteModel> GetSorted(List<PackNoteModel> packNoteModels)
        {
            return packNoteModels.Where(P => !P.Note.DateTimeStatus).ToList();
        }
    }
}
