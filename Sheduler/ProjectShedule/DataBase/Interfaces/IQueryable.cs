using System.Collections.Generic;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IQueryable<out T>
    {
        IEnumerable<T> Query(string query, params object[] args);
    }

}
