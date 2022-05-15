using System;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IQuerybleDateTime<T>
    {
        public List<T> GetForDateTime(DateTime dateTime);
        public List<T> GetForDateTime(DateTime from, DateTime till);
    }
}
