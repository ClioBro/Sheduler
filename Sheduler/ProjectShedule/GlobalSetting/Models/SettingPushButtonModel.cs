using System;
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
    public class OpacityEventSettingModel : SettingPushButtonModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public OpacityEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = "Opacity:";
            Value = _shapeEventSetting.GetOpacity();
            MinValue = 0;
            MaxValue = 1;
            ValueChanged += OnValueChanged;
        }
        private void OnValueChanged(object sender, double e)
        {
            _shapeEventSetting.SetOpacity(e);
            OnPropertyChanged(this, nameof(Value));
        }
    }
    public class CornerRadiusEventSettingModel : SettingPushButtonModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public CornerRadiusEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = "CornerRadius:";
            Value = _shapeEventSetting.GetCornerRadius();
            MinValue = 0;
            MaxValue = 10;
            ValueChanged += OnValueChanged;
        }
        private void OnValueChanged(object sender, double e)
        {
            _shapeEventSetting.SetCornerRadius((float)e);
            OnPropertyChanged(this, nameof(Value));
        }
    }
    public class SizeEventSettingModel : SettingPushButtonModel
    {
        private readonly ShapeEventSetting _shapeEventSetting;
        public SizeEventSettingModel(ShapeEventSetting shapeEventSetting)
        {
            _shapeEventSetting = shapeEventSetting;
            MainText = "Size:";
            Value = _shapeEventSetting.GetSize().Height;
            MinValue = 0;
            MaxValue = 10;
            ValueChanged += OnValueChanged;
        }
        private void OnValueChanged(object sender, double e)
        {
            _shapeEventSetting.SetSize(e);
            OnPropertyChanged(this, nameof(Value));
        }
    }
}
