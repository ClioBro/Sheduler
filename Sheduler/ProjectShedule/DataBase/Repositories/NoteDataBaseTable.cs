using ProjectShedule.Core;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Repositories
{
    public class NoteDataBaseTable : BaseDataBaseTable<Note>, IDateBaseQueryableDateTime<Note>
    {
        private readonly SQLiteConnection _sQLiteConnection;
        public NoteDataBaseTable(SQLiteConnection sQLiteConnection)
            : base(sQLiteConnection)
        {
            _sQLiteConnection = sQLiteConnection;
        }
        public string Name => nameof(TableName.Notes);

        public IEnumerable<Note> GetByDate(DateTime dateTime)
        {
            string appointmentDatePropertyName = nameof(Note.AppointmentDate);

            string query = $"select * from {Name} where {appointmentDatePropertyName} = ?";
            object[] args = { dateTime.Date };

            IEnumerable<Note> notes = _sQLiteConnection.Query<Note>(query, args);

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
            string query = $"select * from {Name} where {appointmentDatePropertyName} >= ? and {appointmentDatePropertyName} < ?";
            object[] args = new object[] { from, till };

            IEnumerable<Note> notes = _sQLiteConnection.Query<Note>(query, args);

            return notes;
        }
        public IEnumerable<Note> GetByDateRange(DateTimeRange dateTimeRange)
        {
            return GetByDateRange(dateTimeRange.Start, dateTimeRange.End);
        }

        public IEnumerable<Note> Query(string query, params object[] args)
        {
            return _sQLiteConnection.Query<Note>(query, args);
        }
    }
}