using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.Models
{
    public class DeleteQuestionSettingModel : SwitchSettingModel
    {
        private readonly DeleteConfirmationSetting _deleteConfirmationSetting;
        public DeleteQuestionSettingModel()
        {
            _deleteConfirmationSetting = new DeleteConfirmationSetting();
            MainText = SettingResources.DeleteQuestionDopTextLabel;
            Value = _deleteConfirmationSetting.AskQuestion;
        }

        public override bool Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                _deleteConfirmationSetting.SetDeleteQuestion(value);
            }
        }
    }
}
