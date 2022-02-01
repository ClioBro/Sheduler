using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Models;
using ProjectShedule.GlobalSetting.ViewModels;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.ViewMdoels
{
    public class SheduleDeleteNotesSettingViewModel : SettingViewModel
    {
        private DeleteConfirmationSetting _deleteConfirmationSetting;

        public SheduleDeleteNotesSettingViewModel()
        {
            _deleteConfirmationSetting = new DeleteConfirmationSetting();

            Title = "SheduleDeleteNotes:";
            DeleteQuestionSetting = new DeleteQuestionSettingModel(_deleteConfirmationSetting);
        }
        public DeleteQuestionSettingModel DeleteQuestionSetting { get; set; }
    }
}
