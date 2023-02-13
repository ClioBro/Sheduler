using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.States
{
    public abstract class TargetState
    {
        public abstract bool IsSelected { get; protected set; }
        public abstract bool IsEnabled { get; protected set; }
        public abstract double BorderWidth { get; protected set; }
        public abstract Color BorderColor { get; protected set; }
        public Color BackGroundColor { get; protected set; }
        public Color TextColor { get; protected set; }

        public abstract TargetState GetOppositeState();
    }
}