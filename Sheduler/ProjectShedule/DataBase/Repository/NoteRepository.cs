using SQLite;
using System;
using System.Collections.Generic;

namespace ProjectShedule.DataNote
{
    public class NoteRepository : IRepositoryDateBase<Note>, IQueryble<Note>
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

        public List<Note> Query(int idConnectedItem)
        {
            throw new NotImplementedException();
        }

        public List<Note> Query(DateTime appointmentDate)
        {
            string tableName = nameof(Table.Notes);
            string propertyName = nameof(Note.AppointmentDate);
            return database.Query<Note>($"select * from {tableName} where {propertyName} = ?", appointmentDate.Date);
        }

        public List<Note> Query(DateTime from, DateTime till)
        {
            if (from > till)
                throw new Exception("Не корректное значение даты и времени");

            string tableName = nameof(Table.Notes);
            string propertyName = nameof(Note.AppointmentDate);
             
            return database.Query<Note>($"select * from {tableName} where {propertyName} >= ? and {propertyName} < ?", from.Date, till.Date);
        }
    }
}
