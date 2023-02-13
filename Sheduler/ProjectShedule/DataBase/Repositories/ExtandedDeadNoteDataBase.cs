using ProjectShedule.Core;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.DataBase.Repositories
{
    public class ExtandedDeadNoteDataBase : IExtandedDeadNoteDataBase
    {
        private readonly SQLiteConnection _dataBase;
        private readonly DeadSmallTaskDataBaseTable _smallTaskDeadDataBaseTable;
        private readonly DeadNoteDataBaseTable _noteDeadDataBaseTable;
        public ExtandedDeadNoteDataBase(SQLiteConnection dataBase)
        {
            _dataBase = dataBase;
            _noteDeadDataBaseTable = new DeadNoteDataBaseTable(_dataBase);
            _smallTaskDeadDataBaseTable = new DeadSmallTaskDataBaseTable(_dataBase);
        }

        public IDeadDataBaseByDateTimeTable<Note> NoteDataBase => _noteDeadDataBaseTable;
        public IDeadDataBase<SmallTask> SmallTaskDataBase => _smallTaskDeadDataBaseTable;

        public void Insert(Note item) => _dataBase.InsertOrReplaceWithChildren(item);
        public void Update(Note item) => _dataBase.UpdateWithChildren(item);
        public void Delete(Note note)
        {
            _dataBase.Delete(note, true);
        }
        public int DeleteItem(int id)
        {
            Note note = GetItem(id);
            if (note is null)
                return 0;
            Delete(note);
            return 1;
        }
        public Note GetItem(int id)
        {
            Note note = _noteDeadDataBaseTable.GetItem(id);
            SetChildren(note);

            return note;
        }
        public IEnumerable<Note> GetAllItems()
        {
            IEnumerable<SmallTask> deletedSmallTasks = _smallTaskDeadDataBaseTable.GetAllItems();
            foreach (SmallTask smallTask in deletedSmallTasks)
                _dataBase.GetChildren(smallTask, true);

            IEnumerable<Note> deletedNotes = _noteDeadDataBaseTable.GetAllItems();
            foreach (Note note in deletedNotes)
                _dataBase.GetChildren(note, true);

            List<Note> resultNotes = new List<Note>();
            resultNotes.AddRange(deletedNotes);

            List<int> notesIds = resultNotes.Select(n => n.Id).ToList();

            foreach (SmallTask smallTask in deletedSmallTasks)
            {
                Note note = smallTask.Note;
                if (note is null)
                    throw new NullReferenceException($"{nameof(note)} is null");
                if (notesIds.Contains(note.Id))
                {
                    continue;
                }
                resultNotes.Add(note);
                notesIds.Add(note.Id);
            }

            return resultNotes;
        }

        public IEnumerable<Note> GetByDates(IEnumerable<DateTime> dates)
        {
            List<Note> notes = new List<Note>();
            foreach (DateTime date in dates)
                notes.AddRange(GetByDate(date));

            return notes;
        }
        public IEnumerable<Note> GetByDate(DateTime appointmentDate)
        {
            IEnumerable<Note> notes = _noteDeadDataBaseTable.GetByDate(appointmentDate);

            SetChildren(notes);

            return notes;
        }
        public IEnumerable<Note> GetByDateRange(DateTimeRange dateTimeRange)
        {
            return GetByDateRange(dateTimeRange.Start, dateTimeRange.End);
        }
        public IEnumerable<Note> GetByDateRange(DateTime from, DateTime till)
        {
            IEnumerable<Note> notes = _noteDeadDataBaseTable.GetByDateRange(from, till);

            SetChildren(notes);

            return notes;
        }

        private void SetChildren(IEnumerable<Note> notes)
        {
            foreach (Note note in notes)
                SetChildren(note);
        }
        private void SetChildren(Note note)
        {
            if (note is null)
                throw new ArgumentNullException(nameof(note));

            IEnumerable<SmallTask> smallTasks = _smallTaskDeadDataBaseTable.GetByNoteId(note.Id);

            foreach (SmallTask smallTask in smallTasks)
            {
                _dataBase.GetChildren(smallTask);
                if (smallTask.Note is null)
                    throw new NullReferenceException("SmallTask children is null");
                //smallTask.NoteId = note.Id;
                //smallTask.Note = note;
            }
            note.SmallTasks.AddRange(smallTasks);
        }

        public void Revive(Note item)
        {
            foreach (var smallTask in item.SmallTasks)
                _smallTaskDeadDataBaseTable.Revive(smallTask);
            _noteDeadDataBaseTable.Revive(item);
        }
    }
}
