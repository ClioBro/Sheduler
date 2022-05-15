using System;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface ISmallTask : ITable, ICloneable
    {
        int IdNote { get; }
        string Text { get; }
        bool Status { get; }
    }
}
