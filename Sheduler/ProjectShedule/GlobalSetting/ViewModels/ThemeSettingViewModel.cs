using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class ThemeSettingViewModel : INotifyPropertyChanged
    {
        private readonly ThemeController _themeController;
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public ThemeSettingViewModel()
        {
            _themeController = App.ThemeController;
            _themeController.ThemeChanged += OnThemeChanged;
        }
        public string Title { get; set; } = "Theme:";

        public string DarkModeText { get; set; } = "DarkMode:";
        public bool DarkModeStatus
        { 
            get => _themeController.CurrentTheme is ThemeController.Theme.Dark;
            set => _themeController.SetThemeOnApp(value ? ThemeController.Theme.Dark : ThemeController.Theme.Light);
        }
        private void OnThemeChanged(ThemeController.Theme oldTheme, ThemeController.Theme newTheme)
        {
            DarkModeStatus = newTheme is ThemeController.Theme.Dark;
            OnPropertyChanged(this, nameof(DarkModeStatus));
        }

    }
}