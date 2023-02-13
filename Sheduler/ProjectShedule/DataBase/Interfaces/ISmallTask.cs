using System;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface ISmallTask : ITableObject, ICloneable
    {
        string Header { get; }
        bool Status { get; }
    }
}
