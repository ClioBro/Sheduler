namespace ProjectShedule.GlobalSetting
{
    public class DeleteConfirmationSetting : Setting<DeleteConfirmationSetting>, IDeleteConfirmation
    {
        private bool _askQuestion;
        public DeleteConfirmationSetting()
        {
            _askQuestion = GetPreference(nameof(DeleteConfirmationSettings.Question), true);
        }
        public bool AskQuestion
        {
            get => _askQuestion;
            private set => _askQuestion = value;
        }
        public void SetDeleteQuestion(bool question)
        {
            SavePreference(nameof(DeleteConfirmationSettings.Question), question);
            _askQuestion = question;
        }
        private enum DeleteConfirmationSettings
        {
            Question
        }

    }
}
