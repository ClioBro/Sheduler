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
    /// <summary>
    ///  Работа с помеченными как "Удалённый". Example: Check -  DeletedDateTime != null and Deleted == true;
    ///  Ищет по фильтру, помеченные как удалённый. Example: <see cref="Note"/>.DeletedDateTime IS NOT NULL
    /// </summary>
    public class ThrashExtandedNoteDataBase : IThrashExtendedNoteDataBase
    {
        private readonly SQLiteConnection _dataBase;
        private readonly IThrashSmallTaskDataBase _thrashSmallTaskDataBase;
        public ThrashExtandedNoteDataBase(SQLiteConnection dataBase)
        {
            _dataBase = dataBase;
            _thrashSmallTaskDataBase = new ThrashSmallTaskDataBase(_dataBase);
        }
        public IDataBase<SmallTask> SmallTaskDateBase => _thrashSmallTaskDataBase;
        public IThrashSmallTaskDataBase ThrashSmallTaskDataBase => _thrashSmallTaskDataBase;

        public void Insert(Note item) => _dataBase.InsertOrReplaceWithChildren(item);
        public void Update(Note item) => _dataBase.InsertOrReplaceWithChildren(item);
        public void Delete(Note note) => _dataBase.Delete(note, true);
        public int DeleteItem(int id)
        {
            Note note = GetItem(id);
            Delete(note);
            return 1;
        }
        public void Revive(Note item)
        {
            item.DeletedDateTime = null;
            foreach (var smallTask in item.SmallTasks)
                ThrashSmallTaskDataBase.Revive(smallTask);
            _dataBase.Update(item);
        }

        public Note GetItem(int id) => _dataBase.Get<Note>(id);
        public IEnumerable<Note> GetAllItems()
        {
            IEnumerable<SmallTask> deletedSmallTasks = SmallTaskDateBase.GetAllItems();

            string tableName = nameof(TableName.Notes);
            string deletedPropertyName = nameof(Note.DeletedDateTime);

            IEnumerable<Note> deletedNotes = _dataBase.Query<Note>(
                $"select * from {tableName} where {deletedPropertyName} IS NOT NULL");

            SetChildren(deletedNotes);

            List<Note> resultNotes = new List<Note>();
            resultNotes.AddRange(deletedNotes);
            List<int> notesIds = resultNotes.Select(n => n.Id).ToList();

            foreach (SmallTask smallTask in deletedSmallTasks)
            {
                Note note = smallTask.Note;
                if (note is null)
                    throw new NullReferenceException($"{nameof(note)} is null");
                if (notesIds.Contains(note.Id)) // note is null, throw ExceptionInvocation
                {
                    continue;
                }
                SetChildren(note);
                resultNotes.Add(note);
                notesIds.Add(note.Id);
            }
            return resultNotes;
        }
        public IEnumerable<Note> GetByDates(IEnumerable<DateTime> dates)
        {
            List<Note> tempList = new List<Note>();
            foreach (DateTime date in dates)
                tempList.AddRange(GetByDate(date));

            return tempList;
        }
        public IEnumerable<Note> GetByDate(DateTime appointmentDate)
        {
            string tableName = nameof(TableName.Notes);
            string appointmentDatePropertyName = nameof(Note.AppointmentDate);
            string deletedPropertyName = nameof(Note.DeletedDateTime);

            IEnumerable<Note> notes = _dataBase.Query<Note>(
                $"select * from {tableName} where {appointmentDatePropertyName} = ? and {deletedPropertyName} IS NOT NULL", appointmentDate.Date);

            SetChildren(notes);

            return notes;
        }
        public IEnumerable<Note> GetByDateRange(DateTimeRange dateTimeRange)
        {
            return GetByDateRange(dateTimeRange.Start, dateTimeRange.End);
        }
        public IEnumerable<Note> GetByDateRange(DateTime from, DateTime till)
        {
            if (from > till)
                throw new Exception("Не корректное значение даты и времени");

            string tableName = nameof(TableName.Notes);
            string appointmentDatePropertyName = nameof(Note.AppointmentDate);
            string deletedPropertyName = nameof(Note.DeletedDateTime);

            IEnumerable<Note> notes = _dataBase.Query<Note>(
                $"select * from {tableName} where {appointmentDatePropertyName} >= ? and {appointmentDatePropertyName} < ? and {deletedPropertyName} IS NOT NULL", from.Date, till.Date);

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
            _dataBase.GetChildren(note);
        }
        public IEnumerable<Note> Query(string query, params object[] args)
        {
            return _dataBase.Query<Note>(query, args);
        }
    }
}
