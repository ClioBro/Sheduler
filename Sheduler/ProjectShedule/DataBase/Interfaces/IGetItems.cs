using System.Collections.Generic;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface IGetItems<T>
    {
        List<T> GetItems();
        T GetItem(int id);
    }
}
