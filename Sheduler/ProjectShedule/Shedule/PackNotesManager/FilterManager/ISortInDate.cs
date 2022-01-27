using ProjectShedule.Shedule.Models;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface ISortInDate<Ttype> : IForVisualElement<Ttype>
    {
        public List<PackNoteModel> GetItems();
    }
    public interface ISortInOrder<Ttype> : IForVisualElement<Ttype>
    {
        public List<PackNoteModel> GetSorted(List<PackNoteModel> packNoteModels);
    }
    public interface IForVisualElement<Ttype>
    {
        public Ttype ThisType { get; }
        public string Text { get; }
        public bool IsSelected { get; set; }
    }
}
