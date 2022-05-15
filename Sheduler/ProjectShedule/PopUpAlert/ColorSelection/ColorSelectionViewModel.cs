using ProjectShedule.Shedule.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    public class ColorSelectionViewModel : INotifyPropertyChanged
    {
        private readonly IColorSelection _colorSelectionModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public ColorSelectionViewModel(IColorSelection colorSelectionModel)
        {
            _colorSelectionModel = colorSelectionModel;

            ColorSelectCommand = new Command<Color>(SetColorInCurrentTarget);

            LineDesignatingButtonCommand = new Command(SelectLineTarget);
            BackGroundDesignatingButtonCommand = new Command(SelectBackgroundTarget);

            _colorSelectionModel.TargetSelected += CurrentTargetChangedHandler;
        }
        public ICommand ColorSelectCommand { get; set; }
        public ICommand LineDesignatingButtonCommand { get; set; }
        public ICommand BackGroundDesignatingButtonCommand { get; set; }
        public string HeaderText => _colorSelectionModel.HeaderText;
        public string BackGroundTargetButtonText => _colorSelectionModel.BackGroundTarget.Text;
        public string LineTargetButtonText => _colorSelectionModel.LineTarget.Text;
        public Color BackGroundTargetButtonBorderColor => _colorSelectionModel.BackGroundTarget.BorderColor;
        public Color LinteTargetButtonBorderColor => _colorSelectionModel.LineTarget.BorderColor;
        private void SetColorInCurrentTarget(Color color) => _colorSelectionModel.SetColorInCurrentTarget(color);
        private void SelectLineTarget()
        {
            _colorSelectionModel.SetCurrentTarget(_colorSelectionModel.LineTarget);
        }
        private void SelectBackgroundTarget()
        {
            _colorSelectionModel.SetCurrentTarget(_colorSelectionModel.BackGroundTarget);
        }
        private void CurrentTargetChangedHandler(object sender, ITarget e)
        {
            OnProppertyChanged(this, nameof(BackGroundTargetButtonBorderColor));
            OnProppertyChanged(this, nameof(LinteTargetButtonBorderColor));
        }
        private void OnProppertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class ColorSelectionPackNoteViewModel : ColorSelectionViewModel
    {
        readonly ColorSelectionPackNoteModel _colorSelectionPackNoteModel;
        public ColorSelectionPackNoteViewModel(ColorSelectionPackNoteModel colorSelectionPackNoteModel) : base(colorSelectionPackNoteModel)
        {
            _colorSelectionPackNoteModel = colorSelectionPackNoteModel;
        }
        public BasePackNoteViewModel PackNoteViewModel => _colorSelectionPackNoteModel.PackNoteViewModel;
    }
}
