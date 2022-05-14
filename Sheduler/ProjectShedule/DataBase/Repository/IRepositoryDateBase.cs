using System;
using System.Collections.Generic;

namespace ProjectShedule.DataNote
{
    public interface IRepositoryDateBase<T> : IGetItems<T>, ISaveItemInDB<T>, IDeleteItemInDB
    {
        //SQLiteConnection database;
    }
    public interface IGetItems<T>
    {
        List<T> GetItems();
        T GetItem(int id);
    }
    public interface ISaveItemInDB<T>
    {
        int SaveItem(T item);
    }
    public interface IDeleteItemInDB
    {
        int DeleteItem(int id);
    }
    public interface IQuerybleId<T>
    {
        public List<T> Query(int idConnectedItem);
    }
    public interface IQuerybleDateTime<T>
    {
        public List<T> Query(DateTime dateTime);
        public List<T> Query(DateTime from, DateTime till);
    }
}
