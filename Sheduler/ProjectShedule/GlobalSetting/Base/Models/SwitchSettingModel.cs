using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Models
{
    public class SwitchSettingModel : BooleanValueElementCellModel
    {
        public SwitchSettingModel() : base(SettingResources.OnLabel, SettingResources.OffLabel)
        {

        }
    }
}
