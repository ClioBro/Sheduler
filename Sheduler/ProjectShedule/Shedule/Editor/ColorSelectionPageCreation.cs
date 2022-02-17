using ProjectShedule.Language.Resources.PopUp.ColorSelection;
using ProjectShedule.PopUpAlert.ColorSelection;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;

namespace ProjectShedule.Shedule.Editor
{
    public class ColorSelectionPageCreation
    {
        private readonly ColorSelectionModel _colorSelectionModel;
        private readonly PackNoteViewModel _packNoteViewModel;
        public ColorSelectionPageCreation(PackNoteModel packNoteModel) : this(new PackNoteViewModel(packNoteModel)) { }
        public ColorSelectionPageCreation(PackNoteViewModel packNoteViewModel)
        {
            _packNoteViewModel = packNoteViewModel;
            _colorSelectionModel = new ColorSelectionModel(
                packNoteViewModel: _packNoteViewModel,
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
