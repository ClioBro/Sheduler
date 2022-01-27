using ProjectShedule.Escaping;
using ProjectShedule.GlobalSetting;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ProjectShedule.AppFlyout.ViewModels
{
    internal class MainFlyoutViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainFlyoutMenuItemViewModel> MenuItems { get; set; }

        public MainFlyoutViewModel()
        {
            MenuItems = GetMyCustomItems();
            SignatureEvent();
            SetDisplayedImageByTheme(App.Theme.CurrentTheme);
        }
        public void SetDisplayedImageByTheme(ThemeController.Theme newTheme)
        {
            
            if (newTheme is ThemeController.Theme.Light)
                foreach (var menuItem in MenuItems)
                    menuItem.DisplayedImage = menuItem.LightImage;

            else
                foreach (var menuItem in MenuItems)
                    menuItem.DisplayedImage = menuItem.DarkImage;
        }
        private void SignatureEvent()
        {
            App.Theme.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(ThemeController.Theme oldTheme, ThemeController.Theme newTheme)
        {
            SetDisplayedImageByTheme(newTheme);
        }
        private ObservableCollection<MainFlyoutMenuItemViewModel> GetMyCustomItems()
        {
            return new ObservableCollection<MainFlyoutMenuItemViewModel>(new[]
            {
                    new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem {
                        Id = 0, Title = "Setting",
                        TargetType = typeof( GlobalSetting.SettingPage) })
                    {
                        LightImage = "setting_Icon.png",
                        DarkImage = "setting_Icon_negate.png"
                    },

                    new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem {
                        Id = 1, Title = "Game",
                        TargetType = typeof( Games.TicTacToePage)})
                    {
                        LightImage = "ticTacToe_icon.png",
                        DarkImage = "ticTacToe_icon_negate.png"
                    },

                    new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem {
                        Id = 2, Title = "Sheduler",
                        TargetType = typeof( Shedule.ShedulePage) })
                    {
                        LightImage = "note_icon.png",
                        DarkImage = "note_icon_negate.png"
                    },
            });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
