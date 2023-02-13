using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Language.Resources.OtherElements;
using ProjectShedule.Shedule.PackNotesManager.FilterManager.Base;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager
{
    public class AllSort : SortBaseNote
    {
        public AllSort(IGetItemsDateTime<Note> getItems) 
            : base(getItems) 
        {
            Text = Filters.AllItems;
        }

        public override IEnumerable<Note> Filter() => GetItemsDateTime.GetAllItems();
    }
}
