using ProjectShedule.Shedule.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager
{
    public interface ISavePackNote
    {
        void Save(IPackNote packNote, bool correct = true);
    }
    public interface IDeletePackNote
    {
        void Delete(IPackNote packNote);
    }
    public interface IGetPackNote
    {
        List<IPackNote> GetAll();
        List<IPackNote> GetForDate(DateTime dateTime);
        List<IPackNote> GetForDate(DateTime first, DateTime second);
    }
    
    public interface IPackNoteRepository : IDeletePackNote, ISavePackNote, IGetPackNote
    {

    }
}
