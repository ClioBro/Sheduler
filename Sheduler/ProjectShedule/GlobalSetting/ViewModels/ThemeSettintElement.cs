namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class ThemeSettintElement : SwitchSettingViewModel
    {
        private readonly ThemeController _themeController;
        public ThemeSettintElement()
        {
            _themeController = App.Theme;
            _themeController.ThemeChanged += OnThemeChanged;
            Title = "Theme:";
            MainText = "DarkMode:";
            SwipeValue = IsOnDarkMode();
            ActionChangedSwipe += OnSwipeChanged;
        }

        private void OnThemeChanged(ThemeController.Theme oldTheme, ThemeController.Theme newTheme)
        {
            SwipeValue = newTheme is ThemeController.Theme.Dark;
            OnPropertyChanged(this, nameof(SwipeValue));
        }

        protected virtual void OnSwipeChanged(bool status)
        {
            _themeController.SetThemeOnApp(status ? ThemeController.Theme.Dark : ThemeController.Theme.Light);
        }
        private bool IsOnDarkMode()
        {
            return _themeController.CurrentTheme is ThemeController.Theme.Dark;
        }
    }
}