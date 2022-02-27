using ProjectShedule.GlobalSetting.Models;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Models
{
    public class DeleteQuestionSettingModel : BooleanValueElementCell
    {
        private readonly DeleteConfirmationSetting _deleteConfirmationSetting;
        public DeleteQuestionSettingModel() : base(SettingResources.OnLabel, SettingResources.OffLabel)
        {
            _deleteConfirmationSetting = new DeleteConfirmationSetting();
            MainText = SettingResources.DeleteQuestionDopTextLabel;
            Value = _deleteConfirmationSetting.AskQuestion;
            ValueChanged += OnValueChanged;
        }

        private void OnValueChanged(object sender, bool value)
        {
            _deleteConfirmationSetting.SetDeleteQuestion(value);
            NotifyVisualUpdate(this, nameof(Value));
            NotifyVisualUpdate(this, nameof(ValueText));
        }
    }
}
