using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Repositories
{
    /// <summary>
    ///  Работа с помеченными как "Удалённый". Example: Check -  DeletedDateTime != null and Deleted == true;
    ///  Ищет по фильтру, помеченные как удалённый. Example: <see cref="SmallTask"/>.DeletedDateTime IS NOT NULL
    /// </summary>
    public class ThrashSmallTaskDataBase : IThrashSmallTaskDataBase
    {
        private readonly SQLiteConnection _dataBase;
        public ThrashSmallTaskDataBase(SQLiteConnection dataBase)
        {
            _dataBase = dataBase;
        }
        public void Insert(SmallTask item) => _dataBase.InsertWithChildren(item);
        public void Update(SmallTask item) => _dataBase.InsertOrReplaceWithChildren(item);
        public void Delete(SmallTask item) => _dataBase.Delete(item);
        public int DeleteItem(int id)
        {
            SmallTask smallTask = GetItem(id);
            Delete(smallTask);
            return 1;
        }

        public IEnumerable<SmallTask> GetAllItems()
        {
            string tableName = nameof(TableName.SmallTasks);
            string deletedPropertyName = nameof(SmallTask.DeletedDateTime);

            IEnumerable<SmallTask> smallTasks = _dataBase.Query<SmallTask>(
                $"select * from {tableName} where {deletedPropertyName} IS NOT NULL");

            SetChildren(smallTasks);

            return smallTasks;
        }
        public SmallTask GetItem(int id) => _dataBase.Get<SmallTask>(id);
        public void Revive(SmallTask item)
        {
            item.DeletedDateTime = null;
            _dataBase.Update(item);
        }

        private void SetChildren(IEnumerable<SmallTask> smalltasks)
        {
            foreach (SmallTask smalltask in smalltasks)
                SetChildren(smalltask);
        }
        private void SetChildren(SmallTask smalltask) => _dataBase.GetChildren(smalltask);
    }
}
