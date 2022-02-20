using ProjectShedule.GlobalSetting.Interfaces;
using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete.ViewMdoels
{
    public class SheduleDeleteNotesSettingViewModel : SettingBox
    {
        public SheduleDeleteNotesSettingViewModel()
        {
            Header = SettingResources.DeleteNotesHeaderLabel;

            DeleteQuestion = new DeleteQuestionSettingModel();
        }
        public IElementCell<bool> DeleteQuestion { get; set; }
    }
}
