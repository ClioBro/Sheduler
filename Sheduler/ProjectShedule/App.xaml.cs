using ProjectShedule.GlobalSetting;
using Xamarin.Forms;

namespace ProjectShedule
{
    public partial class App : Application
    {
        public static Shedule.DataBase.PackNoteData SchedulerDataBase { get; set; }
        public static ThemeController Theme { get; set; }
        public App()
        {
            Theme = new ThemeController();
            Resources.Add(Theme.GetCurrentThemeResource());
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
