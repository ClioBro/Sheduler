using System.Collections.Generic;

namespace ProjectShedule.Core.Sorting
{
    public interface ISortInOrder<T>
    {
        public IEnumerable<T> SortOrderBy(IEnumerable<T> itemSort);
    }
}
