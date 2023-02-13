using System.Collections.Generic;

namespace ProjectShedule.Core.Sorting
{
    public interface IFilter<T>
    {
        public IEnumerable<T> Filter();
    }
}
