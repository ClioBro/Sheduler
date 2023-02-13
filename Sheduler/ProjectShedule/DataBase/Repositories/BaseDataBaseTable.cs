using ProjectShedule.DataBase.Interfaces;
using SQLite;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Repositories
{
    public abstract class BaseDataBaseTable<TType> : IDataBase<TType>
        where TType : new()
    {
        private SQLiteConnection _dataBase;

        public BaseDataBaseTable(SQLiteConnection sQLiteConnection)
        {
            _dataBase = sQLiteConnection;
            _dataBase.CreateTable<TType>();
        }

        public void Insert(TType item)
        {
            _dataBase.Insert(item);
        }
        public void Update(TType item)
        {
            _dataBase.Update(item);
        }

        public void Delete(TType item)
        {
            _dataBase.Delete<TType>(item);
        }
        public int DeleteItem(int id)
        {
            return _dataBase.Delete<TType>(id);
        }

        public IEnumerable<TType> GetAllItems()
        {
            return _dataBase.Table<TType>();
        }
        public TType GetItem(int id)
        {
            return _dataBase.Get<TType>(id);
        }
    }
}