using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;


namespace ProjectShedule.Shedule.Builder
{
    public class SmallTaskModelBuilder : ISmallTaskModelBuilder
    {
        private SmallTask _smallTask;

        public SmallTaskModel Build()
        {
            SmallTask smallTask = _smallTask ?? BuildSmallTask();
            ClearDataFiled();
            return new SmallTaskModel(smallTask);
        }
        public ISmallTaskModelBuilder SetData(SmallTask smallTask)
        {
            _smallTask = smallTask;
            return this;
        }

        private void ClearDataFiled() => _smallTask = null;
        private SmallTask BuildSmallTask() => new SmallTask();
    }

}
