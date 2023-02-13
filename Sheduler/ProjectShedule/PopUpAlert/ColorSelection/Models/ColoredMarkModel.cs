using ProjectShedule.PopUpAlert.ColorSelection.States;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.Models
{
    public class ColoredMarkModel
    {
        public ColoredMarkModel(Color backGroundColor)
            : this()
        {
            ValueColor = backGroundColor;
        }
        private ColoredMarkModel()
        {
            State = new UnSelectedMarkState();
        }

        public Color ValueColor { get; private set; }
        public MarkState State { get; private set; }

        public void SwichState()
        {
            State = State.GetSwichState();
        }
    }
}