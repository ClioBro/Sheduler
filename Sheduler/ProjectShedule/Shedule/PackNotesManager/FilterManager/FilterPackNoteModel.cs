using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.PackNotesManager.FilterManager;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager
{
    public class FilterPackNoteModel
    {
        private protected IEnumerable<IPackNote> Collection;

        public FilterPackNoteModel(PutInOrderNote variant, SortInDate day)
        {
            PutInOrder = variant;
            SortInDate = day;
        }
        public SortInDate SortInDate { get; set; }
        public PutInOrderNote PutInOrder { get; set; }
        
        public IEnumerable<IPackNote> GetFiltered()
        {
            Collection = DayFiltration();
            Collection = VariantFiltration();
            return Collection;
        }
        private IEnumerable<IPackNote> DayFiltration()
        {
            return SortInDate.GetItems();
        }
        private IEnumerable<IPackNote> VariantFiltration()
        {
            return PutInOrder.GetSorted(Collection);
        }
    }
}
