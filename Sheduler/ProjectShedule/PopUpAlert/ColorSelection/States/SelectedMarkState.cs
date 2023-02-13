using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.States
{
    internal class SelectedMarkState : MarkState
    {
        public override bool IsSelected { get; protected set; } = true;
        public override Color BorderColor { get; protected set; } = Color.Black;
        public override double BorderChick { get; protected set; } = 3;

        public override MarkState GetSwichState()
        {
            return new UnSelectedMarkState();
        }
    }
}
