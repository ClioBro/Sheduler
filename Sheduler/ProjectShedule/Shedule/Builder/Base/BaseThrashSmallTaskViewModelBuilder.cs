using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using System.Windows.Input;


namespace ProjectShedule.Shedule.Builder.Base
{
    public abstract class BaseThrashSmallTaskViewModelBuilder<T> : BaseDeletableSmallTaskViewModelBuilder<T>, IReviveViewModelBuilder<BaseThrashSmallTaskViewModelBuilder<T>, T>
        where T : ThrashSmallTaskViewModel
    {
        private ICommand _reviveCommand;
        protected BaseThrashSmallTaskViewModelBuilder(ISmallTaskModelBuilder smallTaskModelBuilder) : base(smallTaskModelBuilder)
        {
        }
        protected ICommand RevieveCommand => _reviveCommand;

        public BaseThrashSmallTaskViewModelBuilder<T> SetReviveCommand(ICommand reviveCommand)
        {
            _reviveCommand = reviveCommand;
            return this;
        }
    }
}
