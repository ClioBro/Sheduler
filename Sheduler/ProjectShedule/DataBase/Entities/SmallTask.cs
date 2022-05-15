using ProjectShedule.DataBase.Entities.Base;
using SQLite;

namespace ProjectShedule.DataBase.Entities
{

    [Table(nameof(TableName.SmallTasks))]
    public class SmallTask : BaseSmallTask
    {
        [PrimaryKey, AutoIncrement]
        [Column("_id")]
        public override int Id { get; set; }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
