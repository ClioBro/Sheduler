using ProjectShedule.DataBase.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Repositories
{
    public interface ITaskRepository : IRepositoryDateBase<SmallTask>, IQuerybleId<SmallTask>
    {

    }
    public class TaskRepository : ITaskRepository
    {
        private readonly SQLiteConnection database;
        public TaskRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<SmallTask>();
        }
        public List<SmallTask> GetItems()
        {
            return database.Table<SmallTask>().ToList();
        }
        public SmallTask GetItem(int id)
        {
            return database.Get<SmallTask>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<SmallTask>(id);
        }
        public int SaveItem(SmallTask item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }

        public List<SmallTask> Query(int idNote)
        {
            string tableName = nameof(TableName.SmallTasks);
            string propertyName = nameof(SmallTask.IdNote);

            return database.Query<SmallTask>($"select * from {tableName} where {propertyName} = ?", idNote);
        }
    }
}
