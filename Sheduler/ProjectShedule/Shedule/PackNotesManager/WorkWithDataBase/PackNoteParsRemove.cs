using ProjectShedule.DataNote;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.WorkWithDataBase
{
    public abstract class PackNoteParsRemove
    {
        protected readonly IRepositoryDateBase<Note> _repositoryNote;
        protected readonly IRepositoryDateBase<SmallTask> _repositoryTask;
        
        public PackNoteParsRemove()
        {
            _repositoryNote = App.SchedulerPackNoteDataBase.Note;
            _repositoryTask = App.SchedulerPackNoteDataBase.Tasks;
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
        public void DeleteInDataBase(Note note)
        {
            _repositoryNote.DeleteItem(note.Id);
        }
        public void DeleteInDataBase(IEnumerable<SmallTask> smallTasks)
        {
            foreach (SmallTask smallTask in smallTasks)
                DeleteInDataBase(smallTask);
        }
        public void DeleteInDataBase(SmallTask task)
        {
            _repositoryTask.DeleteItem(task.Id);
        }

        public Note GetLastSavedNote()
        {
            return _repositoryNote.GetItems().Last();
        }  
    }
}
