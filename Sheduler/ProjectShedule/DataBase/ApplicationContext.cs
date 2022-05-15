using ProjectShedule.DataBase.Repositories;
using System;
using System.IO;

namespace ProjectShedule.DataBase
{
    public class ApplicationContext
    {
        private const string NOTES = "notes.db";
        private const string TASKS = "tasks.db";
        private static INoteRepository _note;
        private static ITaskRepository _task;
        public INoteRepository Note => _note;
        public ITaskRepository Tasks => _task;
        public ApplicationContext()
        {
            SetDataPath(NOTES, TASKS);
        }
        private protected static void SetDataPath(string notePath, string taskPath)
        {
            _note = new NoteRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), notePath));
            _task = new TaskRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), taskPath));
        }
    }
}
