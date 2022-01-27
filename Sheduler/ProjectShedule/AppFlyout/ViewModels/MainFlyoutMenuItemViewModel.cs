using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectShedule.GlobalSetting;
using Xamarin.Forms;

namespace ProjectShedule.AppFlyout
{
    public class MainFlyoutMenuItemViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private readonly MainFlyoutMenuItem _mainFlyoutMenuItem;
        public MainFlyoutMenuItemViewModel(MainFlyoutMenuItem mainFlyoutMenuItem)
        {
            _mainFlyoutMenuItem = mainFlyoutMenuItem;
        }

        public ImageSource DarkImage { get; set; }
        public ImageSource LightImage { get; set; }
        
        public int Id
        {
            get => _mainFlyoutMenuItem.Id;
            set 
            {
                if(_mainFlyoutMenuItem.Id != value)
                {
                    _mainFlyoutMenuItem.Id = value;
                    OnPropertyChanged();
                }
            } 
        }
        public string Title
        {
            get => _mainFlyoutMenuItem.Title;
            set
            {
                if (_mainFlyoutMenuItem.Title != value)
                {
                    _mainFlyoutMenuItem.Title = value;
                    OnPropertyChanged();
                }
            }
        }
        public ImageSource DisplayedImage
        {
            get => _mainFlyoutMenuItem.DisplayedImage;
            set
            {
                if (_mainFlyoutMenuItem.DisplayedImage != value)
                {
                    _mainFlyoutMenuItem.DisplayedImage = value;
                    OnPropertyChanged();
                }
            }
        }
        public Type TargetType
        {
            get => _mainFlyoutMenuItem.TargetType;
            set
            {
                if (_mainFlyoutMenuItem.TargetType != value)
                {
                    _mainFlyoutMenuItem.TargetType = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}