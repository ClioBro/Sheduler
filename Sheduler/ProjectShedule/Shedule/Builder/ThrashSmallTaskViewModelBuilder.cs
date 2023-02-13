using ProjectShedule.Shedule.Builder.Base;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;


namespace ProjectShedule.Shedule.Builder
{
    public sealed class ThrashSmallTaskViewModelBuilder : BaseThrashSmallTaskViewModelBuilder<ThrashSmallTaskViewModel>, IGetClearParamsBuilder<ThrashSmallTaskViewModelBuilder>
    {
        public ThrashSmallTaskViewModelBuilder() : this(new SmallTaskModelBuilder())
        {
        }
        public ThrashSmallTaskViewModelBuilder(ISmallTaskModelBuilder smallTaskModelBuilder)
            : base(smallTaskModelBuilder)
        {
        }

        public override ThrashSmallTaskViewModel Build()
        {
            SmallTaskModel smallTaskModel = SmallTaskModel;
            ResetFields();
            ThrashSmallTaskViewModel thrashSmallTaskViewModel = new ThrashSmallTaskViewModel(smallTaskModel);
            thrashSmallTaskViewModel.DeleteSwipeItem.Command = DeleteCommand;
            thrashSmallTaskViewModel.ReviveSwipeItem.Command = RevieveCommand;
            return thrashSmallTaskViewModel;
        }

        public ThrashSmallTaskViewModelBuilder GetClearParamsBuilder() => new ThrashSmallTaskViewModelBuilder();
    }

}
