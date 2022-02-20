using ProjectShedule.DataNote;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager.WorkWithDataBase;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectShedule.Shedule.PackNotesManager
{
    public class PackNoteDataBaseController : PackNoteParsRemove, IPackNoteRepository
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
    internal class Correction
    {
        private readonly INote _note;
        public Correction(INote note)
        {
            _note = note;
        }
        public void Сorrect(bool replaceEmptyHeader = true, bool reduceGaps = true)
        {
            string header = _note.Header;
            string dopText = _note.DopText;

            if (reduceGaps)
            {
                if (!string.IsNullOrWhiteSpace(dopText))
                    dopText = ReduceGaps(dopText);
                if (!string.IsNullOrWhiteSpace(header))
                    header = ReduceGaps(header);
            }
            if (replaceEmptyHeader && string.IsNullOrWhiteSpace(header))
            {
                header = AssignPartText(dopText, length: 22);
            }

            _note.Header = header;
            _note.DopText = dopText;
        }
        private string AssignPartText(string text, int length = 15)
        {
            return text.Substring(0, text.Length >= length ? length : text.Length);
        }
        private string ReduceGaps(string text)
        {
            return new Regex(@"\s+").Replace(text, " ");
        }
    }
}

