using System.Collections.Generic;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IGetItems<out T>
    {
        IEnumerable<T> GetAllItems();
        T GetItem(int id);
    }
}
