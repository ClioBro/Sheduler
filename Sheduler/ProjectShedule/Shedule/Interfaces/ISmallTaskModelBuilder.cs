using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Models;


namespace ProjectShedule.Shedule.Interfaces
{
    public interface ISmallTaskModelBuilder : IBuilder<SmallTaskModel>, IModelBuilder<ISmallTaskModelBuilder, SmallTask>
    {

    }
}
