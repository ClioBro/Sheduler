using ProjectShedule.GlobalSetting.Models;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Models
{
    public class DeleteQuestionSettingModel : SwitchsSettingModel
    {
        private DeleteConfirmationSetting _deleteConfirmationSetting;
        public DeleteQuestionSettingModel(DeleteConfirmationSetting deleteConfirmationSetting)
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
