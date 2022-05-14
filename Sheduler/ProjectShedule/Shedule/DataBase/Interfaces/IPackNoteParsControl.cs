using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.DataBase.Repositories;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.DataBase.Interfaces
{
    public interface IPackNoteParsControl
    {
        INoteRepository NoteRepository { get; }
        ITaskRepository TaskRepository { get; }
        void SaveInDataBase(BaseNote note);
        void SaveInDataBase(BaseSmallTask task);
        void SaveInDataBase(IEnumerable<BaseSmallTask> tasks, int noteId);
        void DeleteInDataBase(BaseNote note);
        void DeleteInDataBase(IEnumerable<BaseSmallTask> smallTasks);
        void DeleteInDataBase(BaseSmallTask task);
        BaseNote GetLastSavedNote();
    }
}
