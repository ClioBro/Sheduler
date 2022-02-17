using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager.FilterManager;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager
{
    internal partial class FilterPackNote
    {
        private protected List<IPackNote> Collection;

        public FilterPackNote() : this(new PutInOrderByAlphabet(), new ToDaySortInDate()) { }
        public FilterPackNote(PutInOrder variant, SortInDate day)
        {
            PutInOrder = variant;
            SortInDate = day;
        }
        public SortInDate SortInDate { get; set; }
        public PutInOrder PutInOrder { get; set; }
        
        public List<IPackNote> GetFiltered()
        {
            Collection = new List<IPackNote>(DayFiltration());
            Collection = VariantFiltration();
            return Collection;
        }
        private List<IPackNote> DayFiltration()
        {
            return SortInDate.GetItems();
        }
        private List<IPackNote> VariantFiltration()
        {
            return PutInOrder.GetSorted(Collection);
        }
    }
}
