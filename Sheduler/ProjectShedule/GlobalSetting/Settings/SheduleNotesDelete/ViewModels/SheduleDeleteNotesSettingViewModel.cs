using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.ViewMdoels
{
    public class SheduleDeleteNotesSettingViewModel : SettingBoxViewModel
    {
        public SheduleDeleteNotesSettingViewModel()
        {
            Header = SettingResources.DeleteNotesHeaderLabel;

            DeleteQuestion = new DeleteQuestionSettingModel();
        }
        public DeleteQuestionSettingModel DeleteQuestion { get; set; }
    }
}
