using System;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface INote : ITable, ICloneable
    {
        string Header { get; }

        string DopText { get; }

        DateTime CreatedDateTime { get; }

        DateTime AppointmentDate { get; }

        int RepeadIdKey { get; }

        bool Notify { get; }

        bool DateTimeStatus { get; }

        string BackgroundColorKey { get; }

        string LineColorKey { get; }
    }
}
