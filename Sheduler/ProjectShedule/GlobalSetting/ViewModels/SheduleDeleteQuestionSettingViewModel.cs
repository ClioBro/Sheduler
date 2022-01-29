namespace ProjectShedule.GlobalSetting.ViewModels
{
    internal class SheduleDeleteQuestionSettingViewModel
    {
        private readonly DeleteConfirmationSetting _deleteConfirmationSetting;
        public SheduleDeleteQuestionSettingViewModel()
        {
            _deleteConfirmationSetting = new DeleteConfirmationSetting();
        }

        public string Title { get; set; } = "DeleteConfirmation:";

        public string DeleteConfirmationSettingText { get; } = "DeleteQuestion:";
        public bool Status { get => _deleteConfirmationSetting.AskQuestion; set => _deleteConfirmationSetting.SetDeleteQuestion(value); }
    }
}
