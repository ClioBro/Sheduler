using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ProjectShedule
{
    public abstract class BindableBase<TData> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, object> _properties =
            new Dictionary<string, object>();

        private readonly Dictionary<string, PropertyChangedEventArgs> _propertyChangedArgs =
            new Dictionary<string, PropertyChangedEventArgs>();

        protected TProperty GetProperty<TProperty>(TProperty defaultValue = default, [CallerMemberName] string propertyName = "")
        {
            return !_properties.ContainsKey(propertyName)
                ? defaultValue
                : (TProperty)_properties[propertyName];
        }

        protected BindableBase<TData> SetProperty<TProperty>(TProperty value, [CallerMemberName] string propertyName = "")
        {
            if (!_properties.TryGetValue(propertyName, out object storedValue))
            {
                AddProperty(propertyName, value);
            }
            else if (storedValue is object && storedValue.Equals(value))
            {
                return this;
            }

            _properties[propertyName] = value;
            PropertyChanged?.Invoke(this, _propertyChangedArgs[propertyName]);

            return this;
        }

        internal BindableBase<TData> NotifyProperty(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                NotifyProperty(propertyName);
            }
            return this;
        }
        internal BindableBase<TData> NotifyProperty([CallerMemberName] string propertyName = "")
        {
            if (!_propertyChangedArgs.ContainsKey(propertyName))
            {
                _propertyChangedArgs.Add(propertyName, new PropertyChangedEventArgs(propertyName));
            }

            PropertyChanged?.Invoke(this, _propertyChangedArgs[propertyName]);
            return this;
        }
        internal BindableBase<TData> NotifyProperty<TProperty>(Expression<Func<TData, TProperty>> propertyExpression)
        {
            return propertyExpression.Body is MemberExpression property
                ? NotifyProperty(property.Member.Name)
                : throw new ArgumentException($"Expression '{propertyExpression}' does not refer to a property.");
        }

        private void AddProperty(string propertyName, object defaultValue)
        {
            if (!_propertyChangedArgs.ContainsKey(propertyName))
            {
                _propertyChangedArgs.Add(propertyName, new PropertyChangedEventArgs(propertyName));
            }

            _properties.Add(propertyName, defaultValue);
        }
    }
}
