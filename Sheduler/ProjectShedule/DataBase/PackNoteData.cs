using ProjectShedule.DataNote;
using System;
using System.IO;

namespace ProjectShedule.Shedule.DataBase
{
    public class PackNoteData
    {
        private const string NOTES = "notes.db";
        private const string TASKS = "tasks.db";
        private static NoteRepository _note;
        private static TaskRepository _task;
        public IRepositoryDateBase<Note> Note => _note;
        public IRepositoryDateBase<SmallTask> Tasks => _task;
        public PackNoteData()
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
