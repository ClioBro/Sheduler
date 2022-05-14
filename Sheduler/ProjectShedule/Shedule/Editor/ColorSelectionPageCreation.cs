using ProjectShedule.Language.Resources.PopUp.ColorSelection;
using ProjectShedule.PopUpAlert.ColorSelection;
using ProjectShedule.Shedule.ViewModels;

namespace ProjectShedule.Shedule.Editor
{
    public class ColorSelectionPageCreation
    {
        private readonly ColorSelectionPackNoteModel _colorSelectionModel;
        private readonly BasePackNoteViewModel _packNoteViewModel;
        public ColorSelectionPageCreation(BasePackNoteViewModel packNoteViewModel)
        {
            _packNoteViewModel = packNoteViewModel;
            _colorSelectionModel = new ColorSelectionPackNoteModel(
                packNoteViewModel: _packNoteViewModel,
                headerText: ColorSelectionResource.HeaderLabel,
                lineTargetText: ColorSelectionResource.LineTargetButtonText,
                backGroundText: ColorSelectionResource.BackGroundTargetButtonText);
        }
        public IColorSelection ColorSelection => _colorSelectionModel;
        public ColorSelectionPage Create()
        {
            ColorSelectionViewModel colorSelectionViewModel = new ColorSelectionPackNoteViewModel(_colorSelectionModel);

            return new ColorSelectionPage(colorSelectionViewModel);
        }
    }
}
