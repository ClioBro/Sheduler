using ProjectShedule.Shedule.Models;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface ISortInDate<Ttype>
    {
        public List<Ttype> GetItems();
    }
    public interface ISortInOrder<Ttype> 
    {
        public List<Ttype> GetSorted(List<Ttype> itemSort);
    }
}
