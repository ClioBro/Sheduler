using ProjectShedule.Language.Resources.Pages.Setting;

namespace ProjectShedule.GlobalSetting.Models
{
    public class SwitchSettingModel : BooleanValueElementCell
    {
        public SwitchSettingModel() : base(SettingResources.OnLabel, SettingResources.OffLabel)
        {

        }
    }
}
