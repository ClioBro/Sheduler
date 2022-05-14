using System.Collections.Generic;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface ISortInDate<Ttype>
    {
        public IEnumerable<Ttype> GetItems();
    }
    public interface ISortInOrder<Ttype>
    {
        public IEnumerable<Ttype> GetSorted(IEnumerable<Ttype> itemSort);
    }
}
