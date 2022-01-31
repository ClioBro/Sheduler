using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class SwitchsSettingModel
    {
        private bool _status;
        public event EventHandler<bool> StatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public string MainText { get; set; }
        public bool Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    StatusChanged?.Invoke(this, value);
                }
            } 
        }
        protected void OnPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public class SheduleThemeSettingModel : SettingViewModel
    {
        private ThemeController _themeController;
        public SheduleThemeSettingModel()
        {
            _themeController = App.ThemeController;
            Title = "Theme:";
            ThemeSetting = new ThemeSetting(_themeController);
        }
        public ThemeSetting ThemeSetting { get; set; }
    }
    public class ThemeSetting : SwitchsSettingModel
    {
        private ThemeController _themeController;
        public ThemeSetting(ThemeController themeController)
        {
            _themeController = themeController;
            MainText = "DarkMode:";
            Status = _themeController.CurrentTheme is ThemeController.Theme.Dark;
            StatusChanged += OnStatusChanged;
            _themeController.ThemeChanged += OnAppThemeChanged;
        }

        private void OnAppThemeChanged(ThemeController.Theme oldTheme, ThemeController.Theme newTheme)
        {
            Status = newTheme is ThemeController.Theme.Dark;
        }

        private void OnStatusChanged(object sender, bool value)
        {
            _themeController.SetThemeOnApp(value ? ThemeController.Theme.Dark : ThemeController.Theme.Light);
            OnPropertyChanged(this, nameof(Status));
        }
    }
    public class SheduleDeleteNotesSettingModel : SettingViewModel
    {
        private DeleteConfirmationSetting _deleteConfirmationSetting;

        public SheduleDeleteNotesSettingModel()
        {
            _deleteConfirmationSetting = new DeleteConfirmationSetting();

            Title = "SheduleDeleteNotes:";
            DeleteQuestionSetting = new DeleteQuestion(_deleteConfirmationSetting);
        }
        public DeleteQuestion DeleteQuestionSetting { get; set; }
    }
    public class DeleteQuestion : SwitchsSettingModel
    {
        private DeleteConfirmationSetting _deleteConfirmationSetting;
        public DeleteQuestion(DeleteConfirmationSetting deleteConfirmationSetting)
        {
            _deleteConfirmationSetting = deleteConfirmationSetting;
            MainText = "DeleteQuestion:";
            Status = _deleteConfirmationSetting.AskQuestion;
            StatusChanged += OnStatusChanged;
        }

        private void OnStatusChanged(object sender, bool value)
        {
            _deleteConfirmationSetting.SetDeleteQuestion(value);
            OnPropertyChanged(this, nameof(Status));
        }
    }
}
