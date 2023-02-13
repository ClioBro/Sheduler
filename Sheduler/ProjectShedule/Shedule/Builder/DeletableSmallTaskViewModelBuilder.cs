using ProjectShedule.Shedule.Builder.Base;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;


namespace ProjectShedule.Shedule.Builder
{
    public sealed class DeletableSmallTaskViewModelBuilder : BaseDeletableSmallTaskViewModelBuilder<DeletableSmallTaskViewModel>, IGetClearParamsBuilder<DeletableSmallTaskViewModelBuilder>
    {
        public DeletableSmallTaskViewModelBuilder() : this(new SmallTaskModelBuilder())
        {
        }
        public DeletableSmallTaskViewModelBuilder(ISmallTaskModelBuilder smallTaskModelBuilder)
            : base(smallTaskModelBuilder)
        {

        }

        public override DeletableSmallTaskViewModel Build()
        {
            SmallTaskModel smallTaskModel = SmallTaskModel;
            ResetFields();
            DeletableSmallTaskViewModel deletableSmallTaskViewModel = new DeletableSmallTaskViewModel(smallTaskModel);
            deletableSmallTaskViewModel.DeleteSwipeItem.Command = DeleteCommand;
            return deletableSmallTaskViewModel;
        }

        public DeletableSmallTaskViewModelBuilder GetClearParamsBuilder() => new DeletableSmallTaskViewModelBuilder();
    }
}
