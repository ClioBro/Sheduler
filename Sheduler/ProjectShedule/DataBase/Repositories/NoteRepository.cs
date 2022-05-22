
using ProjectShedule.DataBase.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Repositories
{
    public interface INoteRepository : IRepositoryDateBase<Note>, IQuerybleDateTime<Note>
    {

    }
    public class NoteRepository : INoteRepository
    {
        private readonly SQLiteConnection database;
        public NoteRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Note>();
        }
        public List<Note> GetItems()
        {
            return  database.Table<Note>().ToList();
        }
        public Note GetItem(int id)
        {
            return database.Get<Note>(id);
        }
        public int DeleteItem(int id)
        {
            
            return  database.Delete<Note>(id);
        }
        public int SaveItem(Note item)
        {
            if (item.Id != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public List<Note> GetForDates(IEnumerable<DateTime> dateTimes)
        {
            List<Note> tempList = new List<Note>();
            foreach (DateTime dateTime in dateTimes)
                tempList.AddRange(GetForDate(dateTime));

            return tempList;
        }
        public List<Note> GetForDate(DateTime appointmentDate)
        {
            string tableName = nameof(TableName.Notes);
            string propertyName = nameof(Note.AppointmentDate);

            return database.Query<Note>($"select * from {tableName} where {propertyName} = ?", appointmentDate.Date);
        }

        public List<Note> GetForRangeDate(DateTime from, DateTime till)
        {
            if (from > till)
                throw new Exception("Не корректное значение даты и времени");

            string tableName = nameof(TableName.Notes);
            string propertyName = nameof(Note.AppointmentDate);
             
            return database.Query<Note>($"select * from {tableName} where {propertyName} >= ? and {propertyName} < ?", from.Date, till.Date);
        }
    }
}
