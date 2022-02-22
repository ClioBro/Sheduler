using ProjectShedule.GlobalSetting.Interfaces;
using System;
using System.ComponentModel;

namespace ProjectShedule.GlobalSetting.Models
{
    public class ElementCell<T> : IElementCell<T>, INotifyPropertyChanged
    {
        private T _value;
        public event EventHandler<T> ValueChanged;
        public string MainText { get; set; }
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(this, value);
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyVisualUpdate(object sender, string propName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
