using ProjectShedule.GlobalSetting.Resource;
using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Models;
using ProjectShedule.GlobalSetting.ViewModels;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.ViewMdoels
{
    public class SheduleDeleteNotesSettingViewModel : SettingViewModel
    {
        public SheduleDeleteNotesSettingViewModel()
        {
            Title = SettingResources.DeleteNotesHeaderLabel;

            DeleteQuestion = new DeleteQuestionSettingModel();
        }
        public DeleteQuestionSettingModel DeleteQuestion { get; set; }
    }
}
