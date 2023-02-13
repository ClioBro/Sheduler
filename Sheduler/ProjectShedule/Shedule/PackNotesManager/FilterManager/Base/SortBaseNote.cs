using ProjectShedule.Core.RadioButton;
using ProjectShedule.Core.Sorting;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.Base
{
    public abstract class SortBaseNote : RadioButtonItemModel, IFilter<Note>
    {
        public SortBaseNote(IGetItemsDateTime<Note> getItems)
        {
            GetItemsDateTime = getItems;
        }

        protected IGetItemsDateTime<Note> GetItemsDateTime { get; }

        public abstract IEnumerable<Note> Filter();
    }
}
