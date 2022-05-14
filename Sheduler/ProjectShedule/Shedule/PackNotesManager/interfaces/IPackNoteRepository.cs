using ProjectShedule.Shedule.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager
{
    public interface IPackNoteRepository
    {
        public void Save(IPackNote packNote, bool correct = true);
        public void Delete(IPackNote packNote);
        public List<IPackNote> GetAll();
        public List<IPackNote> GetForDate(DateTime dateTime);
        public List<IPackNote> GetForDate(DateTime first, DateTime second);
    }
}
