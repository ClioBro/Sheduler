using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;

namespace ProjectShedule.Shedule.DataBase.Interfaces
{
    public interface IPackNoteDataBaseController :  IDataBaseController<IPackNote> 
    {
        IPackNoteParsControl PartsControl { get; }
    }
}
