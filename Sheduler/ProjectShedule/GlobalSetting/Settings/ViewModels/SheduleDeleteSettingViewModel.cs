using ProjectShedule.GlobalSetting.Base.ViewModel;
using ProjectShedule.GlobalSetting.Settings.Models;
using ProjectShedule.GlobalSetting.ViewModel;
using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Settings.ViewModels
{
    public class SheduleDeleteSettingViewModel : SettingBoxViewModel
    {
        public SheduleDeleteSettingViewModel()
        {
            Header = SettingResources.DeleteNotesHeaderLabel;

            DeleteSwitchQuestionSetting = new SwitchSettingViewModel(new DeleteQuestionSettingModel());
        }

        public SwitchSettingViewModel DeleteSwitchQuestionSetting { get; set; }
    }
}
