using ProjectShedule.DataNote.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.DataNote
{
    public abstract class ParsRemover
    {
        protected readonly IRepositoryDateBase<Note> _repositoryNote;
        protected readonly IRepositoryDateBase<SmallTask> _repositoryTask;
        
        public ParsRemover()
        {
            _repositoryNote = App.SchedulerDataBase.Note;
            _repositoryTask = App.SchedulerDataBase.Tasks;
        }

        public void SaveInDataBase(Note note)
        {
            _repositoryNote.SaveItem(note);
        }
        public void SaveInDataBase(SmallTask task)
        {
            _repositoryTask.SaveItem(task);
        }
        public void SaveInDataBase(IEnumerable<SmallTask> tasks, int noteId)
        {
            foreach (var task in tasks)
            {
                task.IdNote = noteId;
                SaveInDataBase(task);
            }
        }
        public void DeleteInDataBase(ITable<Note> note)
        {
            _repositoryNote.DeleteItem(note.Id);
        }
        public void DeleteInDataBase(IEnumerable<ITable<SmallTask>> smallTasks)
        {
            foreach (ITable<SmallTask> smallTask in smallTasks)
                DeleteInDataBase(smallTask);
        }
        public void DeleteInDataBase(ITable<SmallTask> task)
        {
            _repositoryTask.DeleteItem(task.Id);
        }

        public Note GetLastSavedNote()
        {
            return _repositoryNote.GetItems().Last();
        }

                
    }
}
