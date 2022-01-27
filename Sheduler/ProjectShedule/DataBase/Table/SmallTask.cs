using ProjectShedule.DataNote.Interfaces;
using SQLite;

namespace ProjectShedule.DataNote
{
    [Table(nameof(Table.SmallTasks))]
    public class SmallTask : ITable<SmallTask>, ISmallTask
    {
        [PrimaryKey, AutoIncrement]
        [Column("_id")]
        public int Id { get; set; }

        public int IdNote { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
