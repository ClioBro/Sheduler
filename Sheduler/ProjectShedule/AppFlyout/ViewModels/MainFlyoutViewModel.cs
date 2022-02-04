﻿using ProjectShedule.GlobalSetting.Settings.AppTheme;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.AppFlyout.ViewModels
{
    internal class MainFlyoutViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainFlyoutMenuItemViewModel> MenuItems { get; set; }

        public MainFlyoutViewModel()
        {
            MenuItems = GetMyCustomItems();
            SignatureEvent();
            SetDisplayedImageByTheme(App.ThemeController.CurrentTheme);
        }

        public void SetDisplayedImageByTheme(ThemeController.Theme newTheme)
        {
            switch (newTheme)
            {
                case ThemeController.Theme.Dark:
                    foreach (var menuItem in MenuItems)
                        menuItem.DisplayedImage = menuItem.LightImage;
                    break;

                case ThemeController.Theme.Light:
                    foreach (var menuItem in MenuItems)
                        menuItem.DisplayedImage = menuItem.DarkImage;
                    break;
            }
        }

        private void SignatureEvent()
        {
            App.ThemeController.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(ThemeController.Theme oldTheme, ThemeController.Theme newTheme)
        {
            SetDisplayedImageByTheme(newTheme);
        }
        private ObservableCollection<MainFlyoutMenuItemViewModel> GetMyCustomItems()
        {
            return new ObservableCollection<MainFlyoutMenuItemViewModel>
            {
                new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem {
                    Title = Resources.Lobby.SettingTitle,
                    TargetType = typeof( GlobalSetting.SettingPage) })
                {
                    DarkImage = "setting_Icon.png",
                    LightImage = "setting_Icon_negate.png"
                },

                new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem {
                    Title = Resources.Lobby.GameTitle,
                    TargetType = typeof( Games.TicTacToePage)})
                {
                    DarkImage = "ticTacToe_icon.png",
                    LightImage = "ticTacToe_icon_negate.png"
                },

                new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem {
                    Title = Resources.Lobby.SheduleTitle,
                    TargetType = typeof( Shedule.ShedulePage) })
                {
                    DarkImage = "note_icon.png",
                    LightImage = "note_icon_negate.png"
                }

            };
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
