using ProjectShedule.DataBase.Interfaces;
using SQLite;

namespace ProjectShedule.DataBase.Repositories
{
    public class DataBaseContent
    {
        private readonly SQLiteConnection _dataBase;
        private readonly ExtandedLiveNoteDataBase _extandedLiveNoteDataBase;
        private readonly ExtandedDeadNoteDataBase _extandedDeadNoteDataBase;

        public DataBaseContent(string dataBasePath)
        {
            _dataBase = new SQLiteConnection(dataBasePath);
            _extandedLiveNoteDataBase = new ExtandedLiveNoteDataBase(_dataBase);
            _extandedDeadNoteDataBase = new ExtandedDeadNoteDataBase(_dataBase);
        }
        public IExtandedLiveNoteDataBase ExtendedNoteRepository => _extandedLiveNoteDataBase;
        public IExtandedDeadNoteDataBase ThrashExtendedNoteRepository => _extandedDeadNoteDataBase;
    }
}
