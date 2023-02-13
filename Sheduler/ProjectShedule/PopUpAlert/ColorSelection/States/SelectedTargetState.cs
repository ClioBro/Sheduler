using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.States
{
    internal class SelectedTargetState : TargetState
    {
        public override bool IsSelected { get; protected set; } = true;
        public override bool IsEnabled { get; protected set; } = false;
        public override double BorderWidth { get; protected set; } = 3;
        public override Color BorderColor { get; protected set; } = Color.Black;

        public override TargetState GetOppositeState()
        {
            return new UnSelectedTargetState();
        }
    }
}
