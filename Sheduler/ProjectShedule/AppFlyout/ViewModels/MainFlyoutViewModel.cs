using ProjectShedule.AppFlyout.Models;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using System.Collections.ObjectModel;

namespace ProjectShedule.AppFlyout.ViewModels
{
    internal class MainFlyoutViewModel
    {
        private readonly IThemeController _themeController;

        public MainFlyoutViewModel()
        {
            _themeController = App.ThemeController;

            MenuItems = GetMyCustomItems();
        }
        
        public ObservableCollection<MainFlyoutMenuItemViewModel> MenuItems { get; set; }

        private ObservableCollection<MainFlyoutMenuItemViewModel> GetMyCustomItems()
        {
            return new ObservableCollection<MainFlyoutMenuItemViewModel>
            {
                new SettingMainFlyoutMenuItemViewModel(_themeController),
                new SheduleMainFlyoutMenuItemViewModel(_themeController),
                new RecycleBinMainFlyoutMenuItemViewModel(_themeController)
            };
        }

    }
}
