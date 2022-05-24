using ProjectShedule.AppFlyout.Models;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using ProjectShedule.Language.Resources.Pages.AppFlyout;
using System.Collections.ObjectModel;

namespace ProjectShedule.AppFlyout.ViewModels
{
    internal class MainFlyoutViewModel
    {
        private readonly ThemeController _themeController;
        public ObservableCollection<MainFlyoutMenuItemViewModel> MenuItems { get; set; }

        public MainFlyoutViewModel()
        {
            _themeController = App.ThemeController;
            _themeController.ThemeChanged += OnThemeChanged;

            MenuItems = GetMyCustomItems();
            
            SetDisplayedImageByTheme(App.ThemeController.CurrentTheme);
        }
        private void OnThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            SetDisplayedImageByTheme(e.NewTheme);
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
        private ObservableCollection<MainFlyoutMenuItemViewModel> GetMyCustomItems()
        {
            return new ObservableCollection<MainFlyoutMenuItemViewModel>
            {
                new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem {
                    Title = Lobby.SettingTitle,
                    TargetType = typeof( GlobalSetting.SettingMainPage) })
                {
                    DarkImage = "setting_Icon.png",
                    LightImage = "setting_Icon_negate.png"
                },

                new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem {
                    Title = Lobby.SheduleTitle,
                    TargetType = typeof( Shedule.ShedulePage) })
                {
                    DarkImage = "note_icon.png",
                    LightImage = "note_icon_negate.png"
                },

                //new MainFlyoutMenuItemViewModel(new MainFlyoutMenuItem
                //{
                //    Title = "TestRoom",
                //    TargetType = typeof(Tests.View.TetRoom)
                //}),
            };
        }
    }
}
