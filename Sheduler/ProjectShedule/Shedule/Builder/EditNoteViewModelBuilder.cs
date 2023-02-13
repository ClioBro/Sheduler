
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Builder.Base;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;


namespace ProjectShedule.Shedule.Builder
{
    public class EditNoteViewModelBuilder : BaseNoteViewModelBuilder<EditNoteViewModel>, IEditNoteViewModelBuilder
    {
        public IEditNoteViewModelBuilder GetClearParamsBuilder() => new EditNoteViewModelBuilder();

        protected override EditNoteViewModel BuildViewModel()
        {
            return new EditNoteViewModel(Note ?? new Note());
        }
    }
}
