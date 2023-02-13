using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IShedulePageDataWrite : ISimpleWriteOperation<IHasData<Note>>, ISimpleWriteOperation<IHasData<SmallTask>>
    {

    }
}