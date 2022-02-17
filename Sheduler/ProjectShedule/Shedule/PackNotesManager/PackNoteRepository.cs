using ProjectShedule.DataNote;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager
{
    public class PackNoteRepository : ParsRemover, IPackNoteRepository
    {
        public void Save(IPackNote packNote, bool correct = true)
        {
            Note note = packNote.Note;
            if (correct)
            {
                Correction correctionNote = new Correction(note);
                correctionNote.Сorrect();
            }

            SaveInDataBase(note);
            IEnumerable<IHasSmallTask> hasSmallTasks = packNote.SmallTasks;
            IEnumerable<SmallTask> smallTasks = hasSmallTasks.Select(s => s.SmallTask);
            if (packNote.SmallTasks.Count() > 0)
            {
                if (note.Id == 0)
                    note = GetLastSavedNote();
                SaveInDataBase(smallTasks, noteId: note.Id);
            }
        }
        public void Delete(IPackNote packNote)
        {
            DeleteInDataBase(packNote.Note);
            IEnumerable<IHasSmallTask> hasSmallTasks = packNote.SmallTasks;
            IEnumerable<SmallTask> smallTasks = hasSmallTasks.Select(s => s.SmallTask);
            DeleteInDataBase(smallTasks);
        }
        public List<IPackNote> GetAll()
        {
            return new List<IPackNote>(_repositoryNote.GetItems()
                .Select(T => new PackNoteModel(T, GetTasks(T.Id))));
        }
        public List<IPackNote> GetForDate(DateTime dateTime)
        {
            DateTime minDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            DateTime maxDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(1);
            return GetForDate(minDateTime, maxDateTime);
        }
        public List<IPackNote> GetForDate(DateTime first, DateTime second)
        {
            IQuerybleDateTime<Note> QuerebleNote = _repositoryNote as IQuerybleDateTime<Note>;
            List<Note> selectedNoteByDate = QuerebleNote.Query(first, second);
            if (selectedNoteByDate.Count() <= 0)
                return new List<IPackNote>();

            return new List<IPackNote>(selectedNoteByDate
                .Select(T => new PackNoteModel(T, GetTasks(T.Id))));
        }
        private ObservableCollection<SmallTaskViewModel> GetTasks(int noteId)
        {
            IQuerybleId<SmallTask> QuerebleSmallTask = _repositoryTask as IQuerybleId<SmallTask>;

            return new ObservableCollection<SmallTaskViewModel>(QuerebleSmallTask.Query(noteId)
                .Select(t => new SmallTaskViewModel(t)));
        }
    }
}

