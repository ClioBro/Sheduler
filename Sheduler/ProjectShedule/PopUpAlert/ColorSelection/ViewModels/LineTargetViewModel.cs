using ProjectShedule.PopUpAlert.ColorSelection.States;
using ProjectShedule.Shedule.ViewModels.Interfaces;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.ViewModels
{
    internal class LineTargetViewModel : BaseTargetViewModel
    {
        public LineTargetViewModel(TargetState state)
            : base(state)
        {
            Text = Language.Resources.PopUp.ColorSelection.ColorSelectionResource.LineTargetButtonText;
        }

        public override void ChangeColor(INoteVisualColor noteVisualColor, Color color) => noteVisualColor.LineColor = color;

        public override Color GetColorInTarget(INoteViewModel target)
        {
            return target.LineColor;
        }
    }
}
