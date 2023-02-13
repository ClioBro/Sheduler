using ProjectShedule.PopUpAlert.ColorSelection.States;
using ProjectShedule.Shedule.ViewModels.Interfaces;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.ViewModels
{
    internal class BackTargetViewModel : BaseTargetViewModel
    {
        public BackTargetViewModel(TargetState state)
            : base(state)
        {
            Text = Language.Resources.PopUp.ColorSelection.ColorSelectionResource.BackGroundTargetButtonText;
        }

        public override void ChangeColor(INoteVisualColor noteVisualColor, Color color)
        {
            noteVisualColor.BackGroundColor = color;
        }

        public override Color GetColorInTarget(INoteViewModel target)
        {
            return target.BackGroundColor;
        }
    }
}
