using ProjectShedule.DataBase.BusinessLayer.Entities;
using SQLite;

namespace ProjectShedule.DataBase.Repositories
{
    /// <summary>
    /// База даных без условий
    /// </summary>
    public class SmallTaskDataBaseTable : BaseDataBaseTable<SmallTask>
    {

        public SmallTaskDataBaseTable(SQLiteConnection sQLiteConnection)
            :base(sQLiteConnection)
        {

        }
    }
}