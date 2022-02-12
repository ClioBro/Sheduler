using ProjectShedule.Language.Resources.PopUp.ColorSelection;
using ProjectShedule.PopUpAlert.ColorSelection;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;

namespace ProjectShedule.Shedule.Editor
{
    public class ColorSelectionPageCreation
    {
        private readonly ColorSelectionModel _colorSelectionModel;
        public ColorSelectionPageCreation(PackNoteModel packNoteViewModel)
        {
            _colorSelectionModel = new ColorSelectionModel(
                packNoteViewModel: new PackNoteViewModel(packNoteViewModel),
                headerText: ColorSelectionResource.HeaderLabel,
                lineTargetText: ColorSelectionResource.LineTargetButtonText,
                backGroundText: ColorSelectionResource.BackGroundTargetButtonText);
        }
        public IColorSelection ColorSelection => _colorSelectionModel;
        public ColorSelectionPage Create()
        {
            ColorSelectionViewModel colorSelectionViewModel = new ColorSelectionViewModel(_colorSelectionModel);

            return new ColorSelectionPage(colorSelectionViewModel);
        }
    }
}
