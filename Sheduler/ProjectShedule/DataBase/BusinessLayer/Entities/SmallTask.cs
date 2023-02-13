using ProjectShedule.DataBase.BusinessLayer.Entities.Base;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ProjectShedule.DataBase.BusinessLayer.Entities
{
    [Table(nameof(TableName.SmallTasks))]
    public class SmallTask : BaseSmallTask
    {
        [PrimaryKey, AutoIncrement]
        [Column("_id")]
        public override int Id { get; set; }

        [ForeignKey(typeof(Note))]
        public int NoteId { get; set; }

        [ManyToOne]
        public Note Note { get; set; }
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
