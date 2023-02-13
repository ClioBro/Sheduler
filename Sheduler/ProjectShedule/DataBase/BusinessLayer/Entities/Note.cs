using ProjectShedule.DataBase;
using ProjectShedule.DataBase.BusinessLayer.Entities.Base;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.DataBase.BusinessLayer.Entities
{
    [Table(nameof(TableName.Notes))]
    public class Note : BaseNote
    {
        [PrimaryKey, AutoIncrement]
        [Column("_id")]
        public override int Id { get => base.Id; set => base.Id = value; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public override List<SmallTask> SmallTasks { get => base.SmallTasks; set => base.SmallTasks = value; }
        public override object Clone()
        {
            Note note = MemberwiseClone() as Note;
            note.SmallTasks = SmallTasks.DeepClone().ToList();
            return note;
        }
    }
}
