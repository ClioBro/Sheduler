using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.States
{
    internal class UnSelectedMarkState : MarkState
    {
        public override bool IsSelected { get; protected set; } = false;
        public override Color BorderColor { get; protected set; } = Color.Black;
        public override double BorderChick { get; protected set; } = 0.6;

        public override MarkState GetSwichState()
        {
            return new SelectedMarkState();
        }
    }
}
