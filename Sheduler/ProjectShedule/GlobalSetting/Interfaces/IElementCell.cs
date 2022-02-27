using System;

namespace ProjectShedule.GlobalSetting.Interfaces
{
    public interface IElementCell<T>
    {
        event EventHandler<T> ValueChanged;
        string MainText { get; }
        T Value { get; }
    }
}
