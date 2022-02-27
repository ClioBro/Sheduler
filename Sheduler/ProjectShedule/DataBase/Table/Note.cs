using SQLite;
using System;

namespace ProjectShedule.DataNote
{
    [Table(nameof(Table.Notes))]
    public class Note : INote
    {
        [PrimaryKey, AutoIncrement]
        [Column("_id")]
        public int Id { get; set; }

        public string Header { get; set; }
        public string DopText { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int RepeadIdKey { get; set; }
        public bool Notify { get; set; }
        public bool DateTimeStatus { get; set; }
        public string BackgroundColorKey { get; set; }
        public string LineColorKey { get; set; }
    }
}
