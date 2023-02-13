using ProjectShedule.Core;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.DataBase.Repositories
{
    public class LiveNoteDataBaseTable : IDateBaseQueryableDateTime<Note>, IDataBaseGetByDateTime<Note>
    {
        private readonly SQLiteConnection _dataBase;
        public LiveNoteDataBaseTable(SQLiteConnection sQLiteConnection)
        {
            _dataBase = sQLiteConnection;
            _dataBase.CreateTable<Note>();
        }

        protected SQLiteConnection DataBase => _dataBase;
        public string Name => nameof(TableName.Notes);

        public void Insert(Note item) => _dataBase.Insert(item);
        public void Update(Note item) => _dataBase.Update(item);
        public void Delete(Note item)
        {
            item.DeletedDateTime = DateTime.Now;
            Update(item);
        }
        public int DeleteItem(int id)
        {
            Note note = GetItem(id);
            if (note is null)
                return 0;
            Delete(note);
            return 1;
        }

        public IEnumerable<Note> GetAllItems()
        {
            string deletedPropertyName = nameof(Note.DeletedDateTime);

            string query = $"select * from {Name} where {deletedPropertyName} IS NULL";
            IEnumerable<Note> notes = _dataBase.Query<Note>(query);
            
            return notes;
        }
        public Note GetItem(int id)
        {
            string deletedPropertyName = nameof(Note.DeletedDateTime);
            string idPropertyName = nameof(Note.Id);

            string query = $"select * from {Name} where {idPropertyName} = ? and {deletedPropertyName} IS NULL ";
            Note note = _dataBase.Query<Note>(query, id).FirstOrDefault();

            if (note is null)
                throw new NullReferenceException(nameof(note));

            return note;
        }

        public IEnumerable<Note> GetByDate(DateTime dateTime) 
        {
            string appointmentDatePropertyName = nameof(Note.AppointmentDate);
            string deletedPropertyName = nameof(Note.DeletedDateTime);

            string query = $"select * from {Name} where {appointmentDatePropertyName} >= ? and {appointmentDatePropertyName} < ? and {deletedPropertyName} IS NULL";
            object[] args = {dateTime.Date, dateTime.Date.AddDays(1)};

            IEnumerable<Note> notes = _dataBase.Query<Note>(query, args);

            return notes;
        }

        public IEnumerable<Note> GetByDates(IEnumerable<DateTime> dateTimes)
        {
            List<Note> notes = new List<Note>();
            foreach (DateTime date in dateTimes)
                notes.AddRange(GetByDate(date));

            return notes;
        }
        public IEnumerable<Note> GetByDateRange(DateTime from, DateTime till)
        {
            string appointmentDatePropertyName = nameof(Note.AppointmentDate);
            string deletedPropertyName = nameof(Note.DeletedDateTime);
            
            string query = $"select * from {Name} where {appointmentDatePropertyName} >= ? and {appointmentDatePropertyName} < ? and {deletedPropertyName} IS NULL";
            
            object[] args = new object[] {from, till};
            
            IEnumerable<Note> notes = _dataBase.Query<Note>(query, args);
            
            return notes;
        }

        public IEnumerable<Note> GetByDateRange(DateTimeRange dateTimeRange)
        {
            return GetByDateRange(dateTimeRange.Start, dateTimeRange.End);
        }
        public IEnumerable<Note> Query(string query, params object[] args)
        {
            return _dataBase.Query<Note>(query, args).Where(n => n.DeletedDateTime == null);
        }
    }
}
