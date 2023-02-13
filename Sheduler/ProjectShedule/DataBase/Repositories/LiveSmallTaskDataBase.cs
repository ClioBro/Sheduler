using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.DataBase.Repositories
{
    /// <summary>
    /// Вместо удаления из DataBase, помечает его как удалённый. Example:  DeletedDateTime = DateTime.Now and Deleted = true.
    /// Ищет по фильтру, НЕ помеченные как удалённый. Example: <see cref="SmallTask"/>.DeletedDateTime IS NULL
    /// </summary>
    public class LiveSmallTaskDataBaseTable : IDataBase<SmallTask>
    {
        private readonly SQLiteConnection _dataBase;
        public LiveSmallTaskDataBaseTable(SQLiteConnection sQLiteConnection)
        {
            _dataBase = sQLiteConnection;
            _dataBase.CreateTable<SmallTask>();
        }
        public string Name => nameof(TableName.SmallTasks);
        public void Insert(SmallTask item) => _dataBase.Insert(item);
        public void Update(SmallTask item) => _dataBase.Update(item);
        public void Delete(SmallTask item)
        {
            item.DeletedDateTime = DateTime.Now;
            Update(item);
        }
        public int DeleteItem(int id)
        {
            SmallTask smallTask = GetItem(id);
            if (smallTask is null)
                return 0;
            Delete(smallTask);
            return 1;
        }

        public IEnumerable<SmallTask> GetAllItems()
        {
            string deletedPropertyName = nameof(SmallTask.DeletedDateTime);

            string query = $"select * from {Name} where {deletedPropertyName} IS NULL";
            IEnumerable<SmallTask> smallTasks = _dataBase.Query<SmallTask>(query);

            return smallTasks;
        }
        public SmallTask GetItem(int id)
        {
            string deletedPropertyName = nameof(SmallTask.DeletedDateTime);
            string idPropertyName = nameof(SmallTask.Id);

            string query = $"select * from {Name} where {deletedPropertyName} IS NULL and {idPropertyName} = ?";
            SmallTask smallTask = _dataBase.Query<SmallTask>(query, id).FirstOrDefault();

            if (smallTask is null)
                throw new NullReferenceException($"{nameof(smallTask)} smallTask is null");

            return smallTask;
        }

        public IEnumerable<SmallTask> GetByNoteId(int noteId)
        {
            string noteIDPropertyName = nameof(SmallTask.NoteId);
            string deletedPropertyName = nameof(SmallTask.DeletedDateTime);

            string query = $"select * from {Name} where {noteIDPropertyName} = ? and {deletedPropertyName} IS NULL";

            List<SmallTask> smallTasks = _dataBase.Query<SmallTask>(query, noteId);

            return smallTasks;
        }
    }
}