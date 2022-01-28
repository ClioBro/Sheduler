using System.Collections.ObjectModel;

namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class SettingViewModel
    {
        public SettingViewModel()
        {
            ThemeSettintElement = new ThemeSettintElement();
            DeleteQuestionSettintElement = new DeleteQuestionSettintElement();
            EventCornerRaiusSetting = new SheduleCircleCorcnerRadiusSetting();
            EventOpacitySetting = new SheduleCircleOpacitySetting();
        }
        public ThemeSettintElement ThemeSettintElement { get; set; } 
        public DeleteQuestionSettintElement DeleteQuestionSettintElement { get; set; }
        public SheduleCircleCorcnerRadiusSetting EventCornerRaiusSetting { get; set; }
        public SheduleCircleOpacitySetting EventOpacitySetting { get; set; }

    }
}