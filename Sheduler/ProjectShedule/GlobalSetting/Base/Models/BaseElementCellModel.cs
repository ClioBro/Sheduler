using ProjectShedule.GlobalSetting.Base;
using System;

namespace ProjectShedule.GlobalSetting.Models
{
    public abstract class BaseElementCellModel<T> : IElementCell<T>
    {
        public event EventHandler<T> ValueChanged;
        private T _value;
        
        public string MainText { get; set; }
        public virtual T Value 
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(this, _value);
            }
        }
    }
}
