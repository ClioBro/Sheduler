using ProjectShedule.GlobalSetting.Models;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Models
{
    public class DeleteQuestionSettingModel : SwitchsSettingModel
    {
        private readonly DeleteConfirmationSetting _deleteConfirmationSetting;
        public DeleteQuestionSettingModel()
            : base(falseText: Resources.SettingResources.FalseLabel,
                 trueText: Resources.SettingResources.TrueLabel)
        {
            _deleteConfirmationSetting = new DeleteConfirmationSetting();
            MainText = Resources.SettingResources.DeleteQuestionDopTextLabel;
            Status = _deleteConfirmationSetting.AskQuestion;
            StatusChanged += OnStatusChanged;
        }

        private void OnStatusChanged(object sender, bool value)
        {
            _deleteConfirmationSetting.SetDeleteQuestion(value);
            OnPropertyChanged(this, nameof(Status));
            OnPropertyChanged(this, nameof(StatusText));
        }
    }
}
