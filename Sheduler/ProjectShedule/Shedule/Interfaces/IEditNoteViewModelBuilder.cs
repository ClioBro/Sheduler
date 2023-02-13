using ProjectShedule.Shedule.ViewModels;


namespace ProjectShedule.Shedule.Interfaces
{
    public interface IEditNoteViewModelBuilder : INoteViewModelBuilder<EditNoteViewModel>, IGetClearParamsBuilder<IEditNoteViewModelBuilder>
    {
    }
}
