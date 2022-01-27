using ProjectShedule.DataNote;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager.FilterManager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public PutInOrder PutInOrder { get; set; }
        public SortInDate SortInDate { get; set; }
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
