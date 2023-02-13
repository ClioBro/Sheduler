using System;

namespace ProjectShedule.GlobalSetting.Interfaces
{
    public interface IElementCell<T>
    {
        string MainText { get; }
        T Value { get; }
    }
}
