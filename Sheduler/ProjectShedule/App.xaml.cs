using ProjectShedule.GlobalSetting;
using Xamarin.Forms;

namespace ProjectShedule
{
    public partial class App : Application
    {
        public static Shedule.DataBase.PackNoteData SchedulerDataBase { get; set; }
        public static ThemeController ThemeController { get; set; }
        public App()
        {
            ThemeController = new ThemeController();
            Resources.Add(ThemeController.GetCurrentThemeResource());
            InitializeComponent();
            SchedulerDataBase = new Shedule.DataBase.PackNoteData();
            MainPage = new AppFlyout.Main();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
