using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Interfaces;

namespace ProjectShedule.Shedule.Models.Base
{
    public abstract class BaseSmallTaskViewModel<T> : ISmallTaskViewModelBuilder<T>
        where T : IDemonstrationSmallTaskViewModel
    {
        private readonly ISmallTaskModelBuilder _smallTaskModelBuilder;
        private SmallTaskModel _smallTaskModel;

        public BaseSmallTaskViewModel(ISmallTaskModelBuilder smallTaskModelBuilder)
        {
            _smallTaskModelBuilder = smallTaskModelBuilder;
        }

        protected SmallTaskModel SmallTaskModel => _smallTaskModel ?? _smallTaskModelBuilder.Build();

        protected virtual void ResetFields()
        {
            _smallTaskModel = null;
        }
        public abstract T Build();

        public ISmallTaskViewModelBuilder<T> SetData(SmallTask smallTask)
        {
            _smallTaskModel = _smallTaskModelBuilder.SetData(smallTask).Build();
            return this;
        }
    }
}
