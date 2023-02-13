using ProjectShedule.DataBase;
using ProjectShedule.GlobalSetting.Settings.AppTheme;
using Xamarin.Forms;

namespace ProjectShedule
{
    public partial class App : Application
    {
        internal static ApplicationContext ApplicationContext { get; private set; }
        public static IThemeController ThemeController { get; private set; }
        public App()
        {
            ApplicationContext = new ApplicationContext();
            ThemeController = new ThemeController();
            Resources.Add(ThemeController.GetCurrentThemeResource());
            InitializeComponent();

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
