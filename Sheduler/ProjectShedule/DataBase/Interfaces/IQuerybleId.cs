using System.Collections.Generic;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IQuerybleId<T>
    {
        public List<T> Query(int idConnectedItem);
    }
}
