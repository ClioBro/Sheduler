using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.States
{
    public abstract class MarkState
    {
        public abstract bool IsSelected { get; protected set; }
        public abstract Color BorderColor { get; protected set; }
        public abstract double BorderChick { get; protected set; }
        public abstract MarkState GetSwichState();
    }
}
