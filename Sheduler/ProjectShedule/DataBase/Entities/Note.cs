using ProjectShedule.DataBase.Entities.Base;
using SQLite;

namespace ProjectShedule.DataBase.Entities
{
    [Table(nameof(TableName.Notes))]
    public class Note : BaseNote
    {
        [PrimaryKey, AutoIncrement]
        [Column("_id")]
        public override int Id { get; set; }
        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
