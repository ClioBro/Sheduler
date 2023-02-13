using ProjectShedule.Core;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Repositories
{
    public class ExtandedDeadThreeNoteDataBase : IExtandedDeadNoteDataBase
    {
        private readonly SQLiteConnection _dataBase;
        private readonly DeadSmallTaskDataBaseTable _smalltaskDataBaseTable;
        private readonly DeadNoteDataBaseTable _noteDataBaseTable;
        public ExtandedDeadThreeNoteDataBase(SQLiteConnection dataBase)
        {
            _dataBase = dataBase;
            _noteDataBaseTable = new DeadNoteDataBaseTable(_dataBase);
            _smalltaskDataBaseTable = new DeadSmallTaskDataBaseTable(_dataBase);
        }
        public IDeadDataBaseByDateTimeTable<Note> NoteDataBase => _noteDataBaseTable;
        public IDeadDataBase<SmallTask> SmallTaskDataBase => _smalltaskDataBaseTable;

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
            Note note = _noteDataBaseTable.GetItem(id);
            SetChildren(note);

            return note;
        }
        public IEnumerable<Note> GetAllItems()
        {
            IEnumerable<Note> notes = _noteDataBaseTable.GetAllItems();

            SetChildren(notes);

            return notes;
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
            IEnumerable<Note> notes = _noteDataBaseTable.GetByDate(appointmentDate);

            SetChildren(notes);

            return notes;
        }
        public IEnumerable<Note> GetByDateRange(DateTimeRange dateTimeRange)
        {
            return GetByDateRange(dateTimeRange.Start, dateTimeRange.End);
        }
        public IEnumerable<Note> GetByDateRange(DateTime from, DateTime till)
        {
            IEnumerable<Note> notes = _noteDataBaseTable.GetByDateRange(from, till);

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

            IEnumerable<SmallTask> smallTasks = _smalltaskDataBaseTable.GetByNoteId(note.Id);

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
            _noteDataBaseTable.Revive(item);
            foreach (var small in item.SmallTasks)
            {
                _smalltaskDataBaseTable.Revive(small);
            }
        }
    }
}
