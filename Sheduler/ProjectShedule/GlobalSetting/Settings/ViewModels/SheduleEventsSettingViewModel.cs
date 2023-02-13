using ProjectShedule.GlobalSetting.Base.ViewModel;
using ProjectShedule.GlobalSetting.Settings.Models;
using ProjectShedule.GlobalSetting.Settings.SheduleEvents;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.Calendar.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProjectShedule.GlobalSetting.Settings.ViewModels
{
    public class SheduleEventsSettingViewModel : SettingBoxViewModel, INotifyPropertyChanged
    {
        private readonly ShapeEventSetting _shapeSetting;
        private readonly Dictionary<INotifyPropertyChanged, Action> _viewModelsDictionary = new Dictionary<INotifyPropertyChanged, Action>();

        public SheduleEventsSettingViewModel()
        {
            _shapeSetting = new ShapeEventSetting();

            Header = SettingResources.EventsHeaderLabel;

            OpacityEventSettingViewModel = new ConvertableSlideSettingViewModel(new OpacityEventSettingModel(_shapeSetting));
            CornerRadiusEventSettingViewModel = new ConvertableSlideSettingViewModel(new CornerRadiusEventSettingModel(_shapeSetting));
            SizeEventSettingViewModel = new ConvertableSlideSettingViewModel(new SizeEventSettingModel(_shapeSetting));
            CircleEventViewModel = new CircleEventViewModel(new CircleEventModel());

            UpdateCircleOpacity();
            UpdateCircleCornerRadius();
            UpdateCircleSize();

            _viewModelsDictionary.Add(OpacityEventSettingViewModel, UpdateCircleOpacity);
            _viewModelsDictionary.Add(CornerRadiusEventSettingViewModel, UpdateCircleCornerRadius);
            _viewModelsDictionary.Add(SizeEventSettingViewModel, UpdateCircleSize);

            OpacityEventSettingViewModel.PropertyChanged += UseDictonaryAction;
            CornerRadiusEventSettingViewModel.PropertyChanged += UseDictonaryAction;
            SizeEventSettingViewModel.PropertyChanged += UseDictonaryAction;
        }

        public CircleEventViewModel CircleEventViewModel { get; set; }
        public ConvertableSlideSettingViewModel OpacityEventSettingViewModel { get; set; }
        public ConvertableSlideSettingViewModel CornerRadiusEventSettingViewModel { get; set; }
        public ConvertableSlideSettingViewModel SizeEventSettingViewModel { get; set; }

        private void UseDictonaryAction(object sender, PropertyChangedEventArgs e)
        {
            if (sender is INotifyPropertyChanged notifyPropertyChanged
                && _viewModelsDictionary.ContainsKey(notifyPropertyChanged))
            {
                _viewModelsDictionary[notifyPropertyChanged].Invoke();
            }
        }
        private void UpdateCircleOpacity()
        {
            CircleEventViewModel.Opacity = OpacityEventSettingViewModel.GetConvertToDataValue();
        }
        private void UpdateCircleCornerRadius()
        {
            CircleEventViewModel.CornerRadius = (float)CornerRadiusEventSettingViewModel.GetConvertToDataValue();
        }
        private void UpdateCircleSize()
        {
            CircleEventViewModel.Size = SizeEventSettingViewModel.GetConvertToDataValue();
        }
    }
}
