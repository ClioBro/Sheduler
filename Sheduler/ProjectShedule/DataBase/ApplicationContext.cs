using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.DataBase.Repositories;
using System;
using System.IO;

namespace ProjectShedule.DataBase
{
    public class ApplicationContext
    {
        private const string ULTIMATIVNOTE = "utility.db";

        private static IExtandedLiveNoteDataBase _utilityRepository;
        private static IExtandedDeadNoteDataBase _thrashExtendedNoteRepository;
        public ApplicationContext() => SetDataPath();

        public IExtandedLiveNoteDataBase UtilityExtendedLiveNoteRepository => _utilityRepository;
        public IExtandedDeadNoteDataBase UtilityExtendedDeadNoteRepository => _thrashExtendedNoteRepository;
        private protected void SetDataPath()
        {
            DataBaseContent databaseContent = new DataBaseContent(CombinePath(ULTIMATIVNOTE));
            _utilityRepository = databaseContent.ExtendedNoteRepository;
            _thrashExtendedNoteRepository = databaseContent.ThrashExtendedNoteRepository;
        }
        private string CombinePath(string path) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), path);
    }
}

