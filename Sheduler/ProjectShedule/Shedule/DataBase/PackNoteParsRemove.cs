using ProjectShedule.DataBase;
using ProjectShedule.DataBase.Entities;
using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.DataBase.Repositories;
using ProjectShedule.Shedule.DataBase.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.DataBase
{
    internal class PackNoteParsRemove : IPackNoteParsControl
    {
        protected readonly ApplicationContext _applicationContext;

        public PackNoteParsRemove(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public INoteRepository NoteRepository => _applicationContext.Note;
        public ITaskRepository TaskRepository => _applicationContext.Tasks;

        public void SaveInDataBase(BaseNote note)
        {
            _applicationContext.Note.SaveItem(note as Note);
        }
        public void SaveInDataBase(BaseSmallTask task)
        {
            _applicationContext.Tasks.SaveItem(task as SmallTask);
        }
        public void SaveInDataBase(IEnumerable<BaseSmallTask> tasks, int noteId)
        {
            foreach (var task in tasks)
            {
                task.IdNote = noteId;
                SaveInDataBase(task);
            }
        }
        public void DeleteInDataBase(BaseNote note)
        {
            _applicationContext.Note.DeleteItem(note.Id);
        }
        public void DeleteInDataBase(IEnumerable<BaseSmallTask> smallTasks)
        {
            foreach (SmallTask smallTask in smallTasks)
                DeleteInDataBase(smallTask);
        }
        public void DeleteInDataBase(BaseSmallTask task)
        {
            _applicationContext.Tasks.DeleteItem(task.Id);
        }

        public BaseNote GetLastSavedNote()
        {
            return _applicationContext.Note.GetItems().Last();
        }  
    }
}
