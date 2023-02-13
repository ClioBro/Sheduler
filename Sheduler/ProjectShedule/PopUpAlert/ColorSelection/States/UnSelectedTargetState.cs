using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.States
{
    internal class UnSelectedTargetState : TargetState
    {
        public override bool IsSelected { get; protected set; } = false;
        public override bool IsEnabled { get; protected set; } = true;
        public override double BorderWidth { get; protected set; } = 1;
        public override Color BorderColor { get; protected set; } = Color.Black;
        
        public override TargetState GetOppositeState()
        {
            return new SelectedTargetState();
        }
    }
}
