using ProjectShedule.GlobalSetting.Settings.AppTheme;
using Xamarin.Forms;

namespace ProjectShedule
{
    public partial class App : Application
    {
        public static Shedule.DataBase.PackNoteData SchedulerPackNoteDataBase { get; set; }
        public static ThemeController ThemeController { get; set; }
        public App()
        {
            ThemeController = new ThemeController();
            Resources.Add(ThemeController.GetCurrentThemeResource());
            InitializeComponent();
            SchedulerPackNoteDataBase = new Shedule.DataBase.PackNoteData();
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
