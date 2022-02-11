using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Models
{
    public class DeleteQuestionSettingModel : SwitchsElementModel
    {
        private readonly DeleteConfirmationSetting _deleteConfirmationSetting;
        public DeleteQuestionSettingModel()
        {
            _deleteConfirmationSetting = new DeleteConfirmationSetting();
            MainText = SettingResources.DeleteQuestionDopTextLabel;
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
