using ProjectShedule.DataBase.Interfaces;
using System;

namespace ProjectShedule.DataBase.BusinessLayer.Entities.Base
{
    public abstract class BaseSmallTask : ISmallTask
    {
        public virtual int Id { get; set; }

        public string Header { get; set; }
        public bool Status { get; set; }

        public DateTime? DeletedDateTime { get; set; }
        public bool IsDeleted => DeletedDateTime != null;
        public abstract object Clone();
    }
}
