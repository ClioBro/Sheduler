namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class DeleteQuestionSettintElement : SwitchSettingViewModel
    {
        private readonly DeleteConfirmationSetting _deleteConfirmationSetting;
        public DeleteQuestionSettintElement()
        {
            _deleteConfirmationSetting = new DeleteConfirmationSetting();
            Title = "PupUpQuestion:";
            MainText = "QuestionForDelete:";
            SwipeValue = _deleteConfirmationSetting.AskQuestion;
            ActionChangedSwipe += _deleteConfirmationSetting.SetDeleteQuestion;
        }
    }
}