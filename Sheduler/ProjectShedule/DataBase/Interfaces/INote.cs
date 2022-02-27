using ProjectShedule.DataNote.Interfaces;
using System;

namespace ProjectShedule.DataNote
{
    public interface INote : ITable
    {
        public string Header { get; set; }
        public string DopText { get; set;}
        public DateTime CreatedDateTime { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int RepeadIdKey { get; set; }
        public bool Notify { get; set; }
        public bool DateTimeStatus { get; set; }
        public string BackgroundColorKey { get; set; }
        public string LineColorKey { get; set; }
    }
}