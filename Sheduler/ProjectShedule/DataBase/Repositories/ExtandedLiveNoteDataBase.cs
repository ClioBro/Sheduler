using ProjectShedule.Core;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Repositories
{
    /// <summary>
    /// Вместо удаления из DataBase, помечает его как удалённый. Example:  DeletedDateTime = DateTime.Now and Deleted = true.
    /// Ищет по фильтру, НЕ помеченные как удалённый. Example: <see cref="Note"/>.DeletedDateTime IS NULL
    /// </summary>
    public class ExtandedLiveNoteDataBase : IExtandedLiveNoteDataBase
    {
        private readonly SQLiteConnection _dataBase;
        private readonly LiveSmallTaskDataBaseTable _smalltaskTable;
        private readonly LiveNoteDataBaseTable _noteDataBase;
        public ExtandedLiveNoteDataBase(SQLiteConnection dataBase)
        {
            _dataBase = dataBase;
            _noteDataBase = new LiveNoteDataBaseTable(_dataBase);
            _smalltaskTable = new LiveSmallTaskDataBaseTable(_dataBase);
        }
        public IDataBaseGetByDateTime<Note> NoteDataBase => _noteDataBase;
        public IDataBase<SmallTask> SmallTaskDataBase => _smalltaskTable;

        public void Insert(Note item) => _dataBase.InsertOrReplaceWithChildren(item);
        public void Update(Note item) => _dataBase.UpdateWithChildren(item);
        public void Delete(Note note)
        {
            foreach (SmallTask smallTask in note.SmallTasks)
                _smalltaskTable.Delete(smallTask);
            _noteDataBase.Delete(note);
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
            Note note = _noteDataBase.GetItem(id);
            SetLiveChildren(note);

            return note;
        }
        public IEnumerable<Note> GetAllItems()
        {
            IEnumerable<Note> notes = _noteDataBase.GetAllItems();

            SetLiveChildren(notes);

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
            IEnumerable<Note> notes = _noteDataBase.GetByDate(appointmentDate);

            SetLiveChildren(notes);

            return notes;
        }
        public IEnumerable<Note> GetByDateRange(DateTimeRange dateTimeRange)
        {
            return GetByDateRange(dateTimeRange.Start, dateTimeRange.End);
        }
        public IEnumerable<Note> GetByDateRange(DateTime from, DateTime till)
        {
            IEnumerable<Note> notes = _noteDataBase.GetByDateRange(from, till);
            
            SetLiveChildren(notes);

            return notes;
        }

        private void SetLiveChildren(IEnumerable<Note> notes)
        {
            foreach (Note note in notes)
                SetLiveChildren(note);
        }
        private void SetLiveChildren(Note note)
        {
            if(note is null)
                throw new ArgumentNullException(nameof(note));

            IEnumerable<SmallTask> smallTasks = _smalltaskTable.GetByNoteId(note.Id);
            
            foreach (SmallTask smallTask in smallTasks)
            {
                _dataBase.GetChildren(smallTask);
                if (smallTask.Note is null)
                    throw new NullReferenceException("SmallTask children is null");
            }
            note.SmallTasks.AddRange(smallTasks);
        }
    }
}
