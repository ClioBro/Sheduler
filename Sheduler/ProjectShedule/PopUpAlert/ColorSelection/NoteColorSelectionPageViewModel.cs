using ProjectShedule.Core;
using ProjectShedule.PopUpAlert.ColorSelection.States;
using ProjectShedule.PopUpAlert.ColorSelection.ViewModels;
using ProjectShedule.Shedule.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    public class NoteColorSelectionPageViewModel : BaseViewModel
    {
        private readonly NoteViewModel _noteViewModel;
        private readonly ColoredMarksSelectorViewModel _colorsSelectingBoxViewModel;
        private readonly BaseTargetViewModel[] _targetSelectionViewModels;

        public NoteColorSelectionPageViewModel(NoteViewModel noteViewModel, ColoredMarksSelectorViewModel coloredMarksSelectorViewModel)
        {
            _noteViewModel = noteViewModel;

            _colorsSelectingBoxViewModel = coloredMarksSelectorViewModel;
            _colorsSelectingBoxViewModel.PropertyChanged += OnColorsSelectingBoxViewModel_PropertyChanged;

            ButtonPressedCommand = new Command<BaseTargetViewModel>(OnChangeColorSelectionButtonViewModel);

            var linetarget = new LineTargetViewModel(new SelectedTargetState()) { PressedCommand = ButtonPressedCommand };
            var backtarget = new BackTargetViewModel(new UnSelectedTargetState()) { PressedCommand = ButtonPressedCommand };

            _targetSelectionViewModels = new BaseTargetViewModel[] { linetarget, backtarget, };
            SelectedButton = _targetSelectionViewModels[0];
        }

        public NoteViewModel NoteViewModel { get => _noteViewModel; }
        public ColoredMarksSelectorViewModel SelectingColorsViewModel { get => _colorsSelectingBoxViewModel; }

        #region TargetProperties
        public IEnumerable<BaseTargetViewModel> BaseTargetViewModels { get => _targetSelectionViewModels; }
        private BaseTargetViewModel SelectedButton { get; set; }
        private ICommand ButtonPressedCommand { get; set; }

        #endregion TargetProperties

        private void OnChangeColorSelectionButtonViewModel(BaseTargetViewModel changeColorSelectionButtonViewModel)
        {
            changeColorSelectionButtonViewModel.SwichState();
            SelectedButton?.SwichState();
            SelectedButton = changeColorSelectionButtonViewModel;
            _colorsSelectingBoxViewModel.SelectedColor = SelectedButton.GetColorInTarget(_noteViewModel);
        }
        private void OnColorsSelectingBoxViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SelectingColorsViewModel.SelectedColor))
            {
                SelectedButton.ChangeColor(_noteViewModel, _colorsSelectingBoxViewModel.SelectedColor);
            }
        }

    }
}
