using ProjectShedule.Shedule.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule.PackNotesManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PackNoteView : ContentView
    {
        public PackNoteView()
        {
            InitializeComponent();
        }
        public PackNoteView(BasePackNoteViewModel packNoteModel)
        {
            InitializeComponent();
            BindingContext = packNoteModel;
        }
        public static readonly BindableProperty SwipeIsEnabledProperty =
         BindableProperty.Create(nameof(SwipeIsEnabled), typeof(bool), typeof(PackNoteView), true, BindingMode.TwoWay);
        public bool SwipeIsEnabled
        {
            get => (bool)GetValue(SwipeIsEnabledProperty);
            set => SetValue(SwipeIsEnabledProperty, value);
        }
    }
}