using ProjectShedule.Shedule.ViewModels;


namespace ProjectShedule.Shedule.Interfaces
{
    public interface IDeletableSmallTaskViewModelBuilder : IDeletableViewModelBuilder<IDeletableSmallTaskViewModelBuilder, DeletableSmallTaskViewModel>, ISmallTaskViewModelBuilder<DeletableSmallTaskViewModel>, IGetClearParamsBuilder<IDeletableSmallTaskViewModelBuilder>
    {

    }
}
