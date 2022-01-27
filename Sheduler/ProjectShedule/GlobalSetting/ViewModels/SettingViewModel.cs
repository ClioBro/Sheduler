namespace ProjectShedule.GlobalSetting.ViewModels
{
    public class SettingViewModel
    {
        public SettingViewModel()
        {
            ThemeSettintElement = new ThemeSettintElement();
            DeleteQuestionSettintElement = new DeleteQuestionSettintElement();
            SheduleCircleSettingElement = new SheduleCircleSettingElement();
        }
        public ThemeSettintElement ThemeSettintElement { get; set; } 
        public DeleteQuestionSettintElement DeleteQuestionSettintElement { get; set; }
        public SheduleCircleSettingElement SheduleCircleSettingElement { get; set; }
    }
}