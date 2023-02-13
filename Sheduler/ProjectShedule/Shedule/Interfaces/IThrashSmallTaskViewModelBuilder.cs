using ProjectShedule.Shedule.ViewModels;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IThrashSmallTaskViewModelBuilder : IDeletableViewModelBuilder<IThrashSmallTaskViewModelBuilder, ThrashSmallTaskViewModel>, IReviveViewModelBuilder<IThrashSmallTaskViewModelBuilder, ThrashSmallTaskViewModel>, ISmallTaskViewModelBuilder<ThrashSmallTaskViewModel>, IGetClearParamsBuilder<IThrashSmallTaskViewModelBuilder>
    {

    }
}
