﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.GlobalSetting.Models
{
    public class SettingPushButtonModel :  INotifyPropertyChanged
    {
        private double _valuse;
        public event EventHandler<double> ValueChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public string MainText { get; set; }
        public double Value 
        {
            get => _valuse;
            set 
            {
                if (_valuse != value)
                {
                    _valuse = value;
                    ValueChanged?.Invoke(this, value);
                }
            }  
        }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        protected void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        } 
    }
}