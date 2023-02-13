using System;

namespace ProjectShedule.DataBase.Interfaces
{
    public interface INote : ITableObject, ICloneable
    {
        string Header { get; }
        string DopText { get; }
        DateTime CreatedDateTime { get; }
        DateTime? DeletedDateTime { get; }
        DateTime? AppointmentDate { get; }

        int RepeatIdKey { get; }
        bool Notify { get; }
        bool IsAppointmentDate { get; }
        string BackgroundColorKey { get; }
        string LineColorKey { get; }
    }
}
