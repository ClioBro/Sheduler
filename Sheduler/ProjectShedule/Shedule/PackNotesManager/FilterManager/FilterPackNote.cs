using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager.FilterManager;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager
{
    internal partial class FilterPackNote
    {
        private protected List<PackNoteModel> Collection;

        public FilterPackNote() : this(new PutInOrderByAlphabet(), new ToDaySortInDate()) { }
        public FilterPackNote(PutInOrder variant, SortInDate day)
        {
            PutInOrder = variant;
            SortInDate = day;
        }
        public SortInDate SortInDate { get; set; }
        public PutInOrder PutInOrder { get; set; }
        
        public List<PackNoteModel> GetFiltered()
        {
            Collection = new List<PackNoteModel>(DayFiltration());
            Collection = VariantFiltration();
            return Collection;
        }
        private List<PackNoteModel> DayFiltration()
        {
            return SortInDate.GetItems();
        }
        private List<PackNoteModel> VariantFiltration()
        {
            return PutInOrder.GetSorted(Collection);
        }
    }
}
