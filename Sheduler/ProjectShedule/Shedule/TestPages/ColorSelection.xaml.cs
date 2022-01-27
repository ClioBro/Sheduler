using Xamarin.CommunityToolkit.UI.Views;

using Xamarin.Forms;

namespace ProjectShedule.NotePages
{
    public partial class ColorSelection : Popup<ColorSelection.Resultate>
    {
        private Button LastSelect { get; set; }
        private new readonly Resultate Result;
        public ColorSelection()
        {
            InitializeComponent();
            Result = new Resultate();
        }
        private void SelectLogic(Button button)
        {
            if (LastSelect != null)
            {
                if (LastSelect.BorderColor == Color.Red)
                {
                    LastSelect.BorderColor = Color.Black;
                }
                LastSelect.BorderWidth = 1;
            }
            LastSelect = button;
            button.BorderWidth = 4;
            if (button.BackgroundColor == Color.Black)
            {
                button.BorderColor = Color.Red;
            }
        }
        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Button button = sender as Button;
            SelectLogic(button);
            Result.Color = button.BackgroundColor;
            Result.Message = $"Color Selected! Color: {Result.Color}";
            Dismiss(Result);
        }
        protected override Resultate GetLightDismissResult()
        {
            Result.Message = $"No color selected! Color: {Result.Color}";
            return Result;
        }
        public class Resultate
        {
            public string Message { get; set; }
            public Color Color { get; set; }
        }
    }
}