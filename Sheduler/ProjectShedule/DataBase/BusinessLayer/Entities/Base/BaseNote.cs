using ProjectShedule.DataBase.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjectShedule.DataBase.BusinessLayer.Entities.Base
{
    public abstract class BaseNote : INote
    {
        public virtual int Id { get; set; }
        public string Header { get; set; }

        public string DopText { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public DateTime? AppointmentDate { get; set; }

        public bool IsDeleted => DeletedDateTime != null;
        public int RepeatIdKey { get; set; }

        public bool Notify { get; set; }

        public bool IsAppointmentDate => AppointmentDate != null;

        public string BackgroundColorKey { get; set; }

        public string LineColorKey { get; set; }

        public virtual List<SmallTask> SmallTasks { get; set; } = new List<SmallTask>();
        public abstract object Clone();
    }
}
